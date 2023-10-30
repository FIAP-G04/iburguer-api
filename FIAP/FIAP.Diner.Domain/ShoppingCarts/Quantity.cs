using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.ShoppingCarts;

public sealed record Quantity
{
    private ushort _value;

    private Quantity() {}

    public Quantity(ushort quantity) => Value = quantity;

    public ushort Value
    {
        get => _value;
        private set
        {
            if (value < 1)
                throw new DomainException(Errors.InvalidQuantity);

            _value = value;
        }
    }

    public static implicit operator ushort(Quantity quantity) => quantity.Value;

    public static implicit operator Quantity(ushort value) => new(value);

    public override string ToString() => Value.ToString();

    public bool IsMinimum() => Value == 1;

    public void Increment() => Value++;

    public void Increment(Quantity quantity) => Value = (ushort)(Value + quantity.Value);

    public void Decrement() => Value--;

    public void Decrement(Quantity quantity) => Value = (ushort)(Value - quantity.Value);

    public static class Errors
    {
        public static readonly string InvalidQuantity =
            "Deve ser informado um valor superior a zero para o campo quantidade.";
    }
}