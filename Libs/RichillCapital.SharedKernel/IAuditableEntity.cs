namespace RichillCapital.SharedKernel;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset LastModifiedAt { get; }
}