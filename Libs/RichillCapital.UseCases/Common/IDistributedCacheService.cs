namespace RichillCapital.UseCases.Common;

public interface IDistributedCacheService
{
    Task<TValue?> GetAsync<TValue>(string cacheKey, CancellationToken cancellationToken = default);

    Task SetAsync<TValue>(
        string cacheKey,
        TValue value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default);
}