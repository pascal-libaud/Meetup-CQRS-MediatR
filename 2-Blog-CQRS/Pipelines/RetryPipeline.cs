using MediatR;
using Polly;
using System.Reflection;

namespace _2_Blog_CQRS.Pipelines;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class RetryPolicy : Attribute
{
    public int RetryCount { get; set; } = 5;
    public int RetryDelay { get; set; } = 500;
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
            return await next();

        Func<int, TimeSpan> sleepDurationProvider = i => TimeSpan.FromMilliseconds(i * retryPolicy.RetryDelay);

        return await Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryPolicy.RetryCount, sleepDurationProvider, OnRetry)
            .ExecuteAsync(async () => await next());
    }

    private void OnRetry(Exception exception, TimeSpan timespan, Context _)
    {
        string message = $"Failed to execute handler for {typeof(TRequest).Name}, retrying after {timespan.TotalSeconds}s ({exception.Message})";
        _logger.LogWarning(exception, message);
    }

}