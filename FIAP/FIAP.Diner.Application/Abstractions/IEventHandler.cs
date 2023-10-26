using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Abstractions
{
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellation);
    }
}
