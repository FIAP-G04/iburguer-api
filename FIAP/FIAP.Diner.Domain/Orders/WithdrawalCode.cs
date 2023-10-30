using System.Text;
using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Orders;

public record WithdrawalCode
{
    private static readonly Random random = new();
    private static readonly string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static readonly StringBuilder codeBuilder = new(6);

    public string Code { get; private set; }

    private WithdrawalCode() { }

    public WithdrawalCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(Errors.CannotBeEmptyOrNull);
        }

        Code = code;
    }

    public static implicit operator string(WithdrawalCode code) => code.Code;

    public static implicit operator WithdrawalCode(string code) => new(code);

    public static WithdrawalCode Generate()
    {
        codeBuilder.Clear();

        for (int i = 0; i < 6; i++)
        {
            int index = random.Next(characters.Length);
            codeBuilder.Append(characters[index]);
        }

        return new WithdrawalCode(codeBuilder.ToString());
    }

    public static class Errors
    {
        public static readonly string CannotBeEmptyOrNull = "O código de retirada não pode ser nulo ou vazio";
    }
}