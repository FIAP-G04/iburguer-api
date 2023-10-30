using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Orders;

public record OrderNumber
{
    public int Value { get; private set; }

    private OrderNumber() {}

    public OrderNumber(int value)
    {
        if (value <= 0)
        {
            throw new DomainException(Errors.InvalidNumber);
        }

        Value = value;
    }

    public static implicit operator int(OrderNumber orderNumber) => orderNumber.Value;

    public static implicit operator OrderNumber(int value) => new OrderNumber(value);

    public static class Errors
    {
        public static readonly string InvalidNumber =
            "Deve ser informado um valor superior a zero para o número de pedido.";
    }
}