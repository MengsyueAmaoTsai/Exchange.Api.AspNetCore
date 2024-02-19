
namespace RichillCapital.DataFeeds.Abstractions;

public abstract class DataFeed : IDataFeed
{
    internal protected DataFeed()
    {
        ProviderName = typeof(DataFeed).Name
            .Replace(nameof(DataFeed), string.Empty);
    }

    public string ProviderName { get; private set; }

    public string ConnectionName { get; private set; } = string.Empty;

    public virtual DataFeed ApplyConfiguration(DataFeedConfiguration configuration)
    {
        ProviderName = configuration.ProviderName;
        ConnectionName = configuration.ConnectionName;

        return this;
    }

    public Task ConnectAsync()
    {
        Console.WriteLine("Connecting to data feed...");
        return Task.CompletedTask;
    }
}