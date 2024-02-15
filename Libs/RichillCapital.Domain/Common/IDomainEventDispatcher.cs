using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Common;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearDomainEvents(IEnumerable<IEntity> entities);

    Task DispatchAndClearDomainEvents(IEntity entity);
}