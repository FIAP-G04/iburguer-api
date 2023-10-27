using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Abstractions
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellation) where TEvent : IDomainEvent;
    }
}
