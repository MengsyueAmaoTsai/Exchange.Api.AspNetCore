using MediatR;

namespace RichillCapital.SharedKernel;

public interface IDomainEvent : INotification
{
    DateTimeOffset OccurredTime { get; }
}