using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Cart;

public sealed record Quantity
{
    public int Value { get; private set; }

    public Quantity(int quantity)
    {
        if (quantity < 1)
            throw new DomainException(Errors.InvalidQuantity);

        Value = quantity;
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;

    public static implicit operator Quantity(int value) => new(value);

    public override string ToString() => Value.ToString();

    public bool IsMinimum() => Value == 1;

    public static class Errors
    {
        public static readonly string InvalidQuantity = "Deve ser informado um valor superior a 1 para o campo quantidade.";
    }
}