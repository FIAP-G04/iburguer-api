using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Common;

public sealed record Price
{
    public decimal Amount { get; }

    public Price(decimal amount)
    {
        if (amount <= 0)
        {
            throw new DomainException(Errors.InvalidPrice);
        }

        Amount = amount;
    }

    public override string ToString() => Amount.ToString();

    public static implicit operator decimal(Price price) => price.Amount;

    public static implicit operator Price(decimal value) => new(value);

    public static class Errors
    {
        public static readonly string InvalidPrice = "O preço não pode ter valor igual a zero ou negativo";
    }
}