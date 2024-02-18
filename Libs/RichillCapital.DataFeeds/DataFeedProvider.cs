namespace RichillCapital.DataFeeds;

public sealed class DataFeedProvider
{
    private readonly Dictionary<string, IDataFeed> _dataFeeds = [];

    public DataFeedProvider()
    {
    }

    public Task InitializeAsync()
    {
        // Get all type implementing IDataFeed
        // Create an instance of each type
        // Add each instance to _dataFeeds

        return Task.CompletedTask;
    }
}