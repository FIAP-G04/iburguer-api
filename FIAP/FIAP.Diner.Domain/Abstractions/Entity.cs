namespace FIAP.Diner.Domain.Abstractions;

using System.Collections.Generic;
using System.Linq;

public abstract class Entity<TId> : IEntity
    where TId : class
{
    private readonly ICollection<IDomainEvent> events;

    protected Entity()
    {
        events = new List<IDomainEvent>();
    }

    public TId Id { get; protected set; } = default;

    public IReadOnlyCollection<IDomainEvent> Events => events.ToList().AsReadOnly();

    public void ClearEvents() => events.Clear();

    protected void RaiseEvent(IDomainEvent domainEvent) => events.Add(domainEvent);

    public override bool Equals(object obj)
    {
        if (!(obj is Entity<TId> other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetType() != other.GetType())
        {
            return false;
        }

        if (Id.Equals(default) || other.Id.Equals(default))
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId> first, Entity<TId> second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TId> first, Entity<TId> second) => !(first == second);

    public override int GetHashCode() => (this.GetType().ToString() + Id).GetHashCode();
}