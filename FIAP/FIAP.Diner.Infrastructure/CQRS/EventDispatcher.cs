using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Infrastructure.CQRS
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }


        public async Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellation) where TEvent : IDomainEvent
        {
            var instance = _serviceProvider.GetService(typeof(IEventHandler<TEvent>)) as IEventHandler<TEvent>;

            if (instance == null)
            {
                throw new InvalidOperationException("Não foi possível encontrar nenhum EventHandler para tratar este evento.");
            }

            await instance.Handle(@event, cancellation);
        }
    }
}
