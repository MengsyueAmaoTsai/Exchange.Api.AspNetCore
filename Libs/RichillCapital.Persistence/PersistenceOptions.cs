namespace RichillCapital.Persistence;

public sealed record class PersistenceOptions
{
    public PostgreSqlOptions PostgreSqlOptions { get; init; } = new(string.Empty);
}

public sealed record PostgreSqlOptions(string ConnectionString);