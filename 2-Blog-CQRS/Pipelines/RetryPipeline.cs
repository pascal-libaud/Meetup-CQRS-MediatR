using MediatR;
using Polly;
using System.Reflection;

namespace _2_Blog_CQRS.Pipelines;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class RetryPolicy : Attribute
{
    private int _retryCount = 5;
    private int _retryDelay = 500;

    public int RetryCount
    {
        get => _retryCount;
        set
        {
            if (value < 1)
            {
                throw new ArgumentException("Retry count must be at least 1", nameof(value));
            }
            _retryCount = value;
        }
    }

    public int RetryDelay
    {
        get => _retryDelay;
        set
        {
            if (value < 1)
            {
                throw new ArgumentException("Retry delay must be at least 1ms", nameof(value));
            }
            _retryDelay = value;
        }
    }
}

public class RetryPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<RetryPipeline<TRequest, TResponse>> _logger;

    public RetryPipeline(ILogger<RetryPipeline<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var retryPolicy = typeof(TRequest).GetCustomAttribute<RetryPolicy>();

        if (retryPolicy == null)
        {
            return await next();
        }

        return await Policy.Handle<Exception>()
            .WaitAndRetryAsync(
                retryPolicy.RetryCount,
                i => TimeSpan.FromMilliseconds(i * retryPolicy.RetryDelay),
                (exception, timespan, _) => _logger.LogWarning(exception, $"Failed to execute handler for {typeof(TRequest).Name}, retrying after {timespan.TotalSeconds}s ({exception.Message})")
            )
            .ExecuteAsync(async () => await next());
    }
}