namespace FIAP.Diner.Application.Abstractions;

public interface ICommandDispatcher
{
    Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation);
}