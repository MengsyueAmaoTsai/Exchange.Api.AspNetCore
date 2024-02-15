using MediatR;

using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel;

namespace RichillCapital.UseCases.Common;

public sealed class MediatRDomainEventDispatcher(IPublisher _publisher) :
    IDomainEventDispatcher
{
    public async Task DispatchAndClearDomainEvents(IEnumerable<IEntity> entities)
    {
        foreach (var entity in entities)
        {
            await DispatchAndClearDomainEvents(entity);
        }
    }

    public async Task DispatchAndClearDomainEvents(IEntity entity)
    {
        var events = entity
            .GetDomainEvents()
            .ToArray();

        entity.ClearDomainEvents();

        foreach (var domainEvent in events)
        {
            await _publisher
                .Publish(domainEvent)
                .ConfigureAwait(false);
        }
    }
}