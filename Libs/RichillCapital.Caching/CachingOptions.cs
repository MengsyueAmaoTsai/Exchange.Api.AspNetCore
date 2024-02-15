namespace RichillCapital.Caching;

public sealed record class CachingOptions
{
    public RedisOptions RedisOptions { get; init; } = new(string.Empty, string.Empty);
}

public sealed record RedisOptions(
    string ConnectionString,
    string InstanceName);