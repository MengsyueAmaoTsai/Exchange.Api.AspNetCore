namespace RichillCapital.DataFeeds.Exceptions;

public sealed class DataFeedNotSupportedException : Exception
{
    public DataFeedNotSupportedException(string connectionName, string providerName)
        : base($"Data feed provider with name '{providerName}' not supported. ConnectionName: '{connectionName}'")
    {
    }
}