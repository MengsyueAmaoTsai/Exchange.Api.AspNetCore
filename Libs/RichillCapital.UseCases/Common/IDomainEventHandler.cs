using MediatR;

using RichillCapital.SharedKernel;

namespace RichillCapital.UseCases.Common;

public interface IDomainEventHandler<TDomainEvent> :
    INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    new Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
}