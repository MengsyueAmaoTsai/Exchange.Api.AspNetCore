using Microsoft.Extensions.Caching.Memory;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Caching;

internal sealed class MemoryCacheService(
    IMemoryCache _memoryCache) :
    IMemoryCacheService
{
    private static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(5);

    public async Task<TValue> GetOrCreateAsync<TValue>(
        string cacheKey,
        Func<CancellationToken, Task<TValue>> factory,
        TimeSpan? cacheDuration,
        CancellationToken cancellationToken = default)
    {
        TValue? result = await _memoryCache.GetOrCreateAsync(
            cacheKey,
            entry =>
            {
                entry.SetAbsoluteExpiration(cacheDuration ?? DefaultCacheDuration);
                return factory(cancellationToken);
            });

        return result;
    }
}