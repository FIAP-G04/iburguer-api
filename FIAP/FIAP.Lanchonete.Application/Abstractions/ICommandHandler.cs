namespace FIAP.Diner.Application.Abstractions;

public interface ICommandHandler<in TCommand>
{
    Task Handle(TCommand command, CancellationToken cancellation);
}