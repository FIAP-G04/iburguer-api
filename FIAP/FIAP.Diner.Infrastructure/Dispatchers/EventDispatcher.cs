using System.Reflection;
using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Infrastructure.Dispatchers;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider provider) => _serviceProvider = provider;


    public async Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellation)
        where TEvent : IDomainEvent
    {
        var eventType = @event.GetType();
        var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

        dynamic instance = _serviceProvider.GetService(handlerType);

        if (instance == null)
            throw new InvalidOperationException(
                "Não foi possível encontrar nenhum EventHandler para tratar este evento.");

        await instance.Handle(@event, cancellation);
    }
}