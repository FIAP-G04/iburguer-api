namespace FIAP.Diner.Domain.Abstractions;

public interface IIdentifier<out TId> where TId : struct
{
    TId Value { get; }
}