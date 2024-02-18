namespace RichillCapital.DataFeeds;

public sealed record class DataFeedOptions
{
    public bool ConnectOnAppStart { get; init; } = true;
}
