using FIAP.Diner.Application.Abstractions;

namespace FIAP.Diner.Infrastructure.CQRS;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider provider) => _serviceProvider = provider;

    public async Task Dispatch<T>(T command, CancellationToken cancellation)
    {
        var instance =
            _serviceProvider.GetService(typeof(IHandler<T>)) as IHandler<T>;

        if (instance == null)
            throw new InvalidOperationException(
                "Não foi possível encontrar nenhum CommandHandler para tratar este comando.");

        await instance.Handle(command, cancellation);
    }
}