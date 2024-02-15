using Microsoft.Extensions.Caching.Memory;

namespace RichillCapital.UseCases.Common;

public interface IMemoryCacheService
{
    Task<TValue> GetOrCreateAsync<TValue>(
        string cacheKey,
        Func<CancellationToken, Task<TValue>> factory,
        TimeSpan? cacheDuration,
        CancellationToken cancellationToken = default);
}