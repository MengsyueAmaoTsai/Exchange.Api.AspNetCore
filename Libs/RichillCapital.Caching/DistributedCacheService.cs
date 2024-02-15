using System.Buffers;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Caching;

internal sealed class DistributedCacheService(IDistributedCache _cache) :
    IDistributedCacheService
{
    public async Task<TValue?> GetAsync<TValue>(
        string cacheKey,
        CancellationToken cancellationToken = default)
    {
        var bytes = await _cache.GetAsync(cacheKey, cancellationToken);

        return bytes is null ?
            default :
            Deserialize<TValue>(bytes);
    }

    public Task RemoveAsync(
        string cacheKey,
        CancellationToken cancellationToken = default) =>
        _cache.RemoveAsync(cacheKey, cancellationToken);

    public Task SetAsync<TValue>(
        string cacheKey,
        TValue value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default) =>
        _cache.SetAsync(
            cacheKey,
            Serialize(value),
            expiration is not null ?
                DistributedCacheOptions.WithExpiration(expiration.Value) :
                DistributedCacheOptions.Default,
            cancellationToken);

    private static TValue Deserialize<TValue>(byte[] bytes) =>
        JsonSerializer.Deserialize<TValue>(bytes)!;

    private static byte[] Serialize<TValue>(TValue value)
    {
        var buffer = new ArrayBufferWriter<byte>();

        using var writer = new Utf8JsonWriter(buffer);

        JsonSerializer.Serialize(writer, value);

        return buffer.WrittenSpan.ToArray();
    }
}

internal static class DistributedCacheOptions
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    public static readonly DistributedCacheEntryOptions Default = WithExpiration(DefaultExpiration);

    public static DistributedCacheEntryOptions WithExpiration(TimeSpan expiration) => new()
    {
        AbsoluteExpirationRelativeToNow = expiration,
    };
}