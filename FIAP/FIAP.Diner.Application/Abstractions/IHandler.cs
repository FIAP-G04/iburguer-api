namespace FIAP.Diner.Application.Abstractions;

public interface IHandler<in TCommand>
{
    Task Handle(TCommand command, CancellationToken cancellation);
}