namespace RichillCapital.DataFeeds.Abstractions;

public interface IDataFeed
{
    string ProviderName { get; }

    string ConnectionName { get; }

    DataFeed ApplyConfiguration(DataFeedConfiguration configuration);

    Task ConnectAsync();
}