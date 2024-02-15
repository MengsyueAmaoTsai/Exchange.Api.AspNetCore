namespace RichillCapital.SharedKernel;

public interface IEntity
{
    IEnumerable<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();

    void RegisterDomainEvent(IDomainEvent domainEvent);
}