using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Common;

public abstract record class DomainEvent :
    IDomainEvent
{
    public DateTimeOffset OccurredTime { get; protected set; }
}