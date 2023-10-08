using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Cart;

public sealed record Quantity
{
    public ushort Value { get; private set; }

    public Quantity(ushort quantity)
    {
        if (quantity < 1)
            throw new DomainException(Errors.InvalidQuantity);

        Value = quantity;
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;

    public static implicit operator Quantity(int value) => new(value);

    public override string ToString() => Value.ToString();

    public static class Errors
    {
        public static readonly string InvalidQuantity = "Deve ser informado um valor superior a 1 para o campo quantidade.";
    }
}