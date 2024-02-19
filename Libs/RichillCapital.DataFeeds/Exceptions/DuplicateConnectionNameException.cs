namespace RichillCapital.DataFeeds.Exceptions;

public sealed class DuplicateConnectionNameException : Exception
{
    public DuplicateConnectionNameException(string connectionName)
        : base($"IDataFeed with KeyedService '{connectionName}' connection name already exists.")
    {
    }
}