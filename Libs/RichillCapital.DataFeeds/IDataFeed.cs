using RichillCapital.DataFeeds.Abstractions;

namespace RichillCapital.DataFeeds;

public interface IDataFeed
{
    string ProviderName { get; }

    string ConnectionName { get; }

    DataFeed ApplyConfiguration(DataFeedConfiguration configuration);

    Task ConnectAsync();
}