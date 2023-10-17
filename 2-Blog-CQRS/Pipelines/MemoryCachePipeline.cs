using _2_Blog_CQRS.Common;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace _2_Blog_CQRS.Pipelines;

public class MemoryCachePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCachePipeline(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not IQuery<TResponse>)
            return await next();

        if (!_memoryCache.TryGetValue(request, out TResponse? result))
        {
            result = await next();
            _memoryCache.Set(request, result, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60)));
        }

        return result!;
    }
}