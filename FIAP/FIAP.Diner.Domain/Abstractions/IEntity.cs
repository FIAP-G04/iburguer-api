namespace FIAP.Diner.Domain.Abstractions;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void ClearEvents();
}