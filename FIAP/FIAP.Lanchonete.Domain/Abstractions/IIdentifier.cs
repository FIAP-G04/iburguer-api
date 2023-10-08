namespace FIAP.Diner.Domain.Common.Identifiers;

public interface IIdentifier<out TId> where TId : struct
{
    TId Value { get; }
}