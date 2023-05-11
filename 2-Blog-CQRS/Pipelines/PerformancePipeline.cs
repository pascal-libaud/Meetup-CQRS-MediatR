using MediatR;
using System.Diagnostics;

namespace _2_Blog_CQRS.Pipelines
{
    public class PerformancePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<PerformancePipeline<TRequest, TResponse>> _logger;

        public PerformancePipeline(ILogger<PerformancePipeline<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                return await next();
            }
            finally
            {

                stopwatch.Stop();
                _logger.LogInformation($"Handled {typeof(TRequest).Name} in {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
