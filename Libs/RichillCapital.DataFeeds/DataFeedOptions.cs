namespace RichillCapital.DataFeeds;

public sealed record class DataFeedOptions
{
    public bool ConnectOnAppStart { get; init; } = true;

    public IEnumerable<DataFeedConfiguration> Configurations { get; init; } = [];
}


public sealed record class DataFeedConfiguration
{
    public string ProviderName { get; init; } = string.Empty;

    public string ConnectionName { get; init; } = string.Empty;

    public bool IsEnabled { get; init; }

    public IDictionary<string, string> Arguments { get; init; } = new Dictionary<string, string>();
}