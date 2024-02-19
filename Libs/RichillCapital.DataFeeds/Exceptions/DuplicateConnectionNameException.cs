namespace RichillCapital.DataFeeds.Exceptions;

public sealed class DuplicateConnectionNameException : Exception
{
    public DuplicateConnectionNameException(string providerName, string connectionName)
        : base($"IDataFeed with KeyedService for '{providerName}' provider and '{connectionName}' connection name already exists.")
    {
    }
}