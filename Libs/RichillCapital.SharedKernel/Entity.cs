namespace RichillCapital.SharedKernel;

public abstract class Entity<TEntityId> :
    IEntity,
    IEquatable<Entity<TEntityId>>
    where TEntityId : class
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(TEntityId id)
    {
        Id = id;
    }

    public TEntityId Id { get; private init; }

    public static bool operator ==(Entity<TEntityId>? a, Entity<TEntityId>? b) =>
        (a is null && b is null) ||
        (a is not null &&
            b is not null &&
            a.Equals(b));

    public static bool operator !=(Entity<TEntityId>? a, Entity<TEntityId>? b) => !(a == b);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IEnumerable<IDomainEvent> GetDomainEvents() =>
        _domainEvents.AsReadOnly();

    public void RegisterDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public override bool Equals(object? obj) =>
        obj is not null &&
        (ReferenceEquals(this, obj) ||
            (obj.GetType() == GetType() &&
            obj is Entity<TEntityId> other &&
            Id == other.Id));

    public bool Equals(Entity<TEntityId>? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}