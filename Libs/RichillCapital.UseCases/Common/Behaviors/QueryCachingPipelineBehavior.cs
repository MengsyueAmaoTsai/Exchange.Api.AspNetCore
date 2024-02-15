using MediatR;

namespace RichillCapital.UseCases.Common.Behaviors;

internal sealed class QueryCachingPipelineBehavior<TCachedQuery, TResult>(
    IMemoryCacheService _cacheService) :
    IPipelineBehavior<TCachedQuery, TResult>
    where TCachedQuery : ICachedQuery<TResult>
{
    public async Task<TResult> Handle(
        TCachedQuery request,
        RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken) =>
        await _cacheService.GetOrCreateAsync(
            request.CacheKey,
            _ => next(),
            request.CacheDuration,
            cancellationToken);
}