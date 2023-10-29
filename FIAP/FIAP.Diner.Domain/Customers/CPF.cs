namespace FIAP.Diner.Domain.Customers;

public record CPF
{
    private const int checkDigitsLength = 2;
    private const int numericalDigitsLength = 9;
    private const int cpfLength = checkDigitsLength + numericalDigitsLength;

    private CPF() { }

    public CPF(string number)
    {
        Validate(number);

        Number = number;
    }

    public string NumericalDigits => Number.AsSpan(0, numericalDigitsLength).ToString();

    public string CheckDigits => Number.AsSpan(numericalDigitsLength, checkDigitsLength).ToString();

    public string Number { get; private set; }

    public override string ToString() => Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");

    public static implicit operator string(CPF value) => value.Number;

    public static implicit operator CPF(string value) => new(value);

    private void Validate(string number)
    {
        if (number is null || number == string.Empty)
            throw new InvalidOperationException(
                "Não é possível criar um CPF a partir de um valor nulo.");

        if (!(IsNumeric(number) && IsValidLegth(number)))
            throw new InvalidOperationException(
                $"Era esperado uma string numérica de {cpfLength} dígitos");

        if (AllDigitsAreEqual(number))
            throw new InvalidOperationException(
                "A cadeia de caracteres informada não corresponde a um CPF válido.");

        if (IsValidCheckDigits(number))
            throw new InvalidOperationException(
                "A cadeia de caracteres informada não corresponde a um CPF válido.");
    }

    private bool AllDigitsAreEqual(string value) => value.All(digit => digit == value[0]);

    private bool IsNumeric(string value) => value.All(char.IsNumber);

    private bool IsValidLegth(string value) => value.Length == cpfLength;

    private bool IsValidCheckDigits(string cpf)
    {
        var numericalDigits = cpf.Substring(0, 9).ToCharArray().Select(c => int.Parse(c.ToString()))
            .ToArray();

        var checkDigits = CalculateCheckDigits(numericalDigits);

        return !cpf.Substring(numericalDigitsLength, checkDigitsLength).Equals(checkDigits);
    }

    private string CalculateCheckDigits(int[] numbers)
    {
        var sum = 0;

        for (var i = 0; i < numbers.Length; i++) sum += numbers[i] * (i + 1);

        var firstDigit = GetDigitFromRemainder(sum % cpfLength);

        sum = 0;

        var numbersPlusFirstCheckDigit = numbers.Append(firstDigit).ToArray();

        for (var i = 0; i < numbersPlusFirstCheckDigit.Length; i++)
            sum += numbersPlusFirstCheckDigit[i] * i;

        var secondDigit = GetDigitFromRemainder(sum % cpfLength);

        return $"{firstDigit}{secondDigit}";
    }

    private int GetDigitFromRemainder(int remainder) => remainder >= 10 ? default : remainder;

    public static class Errors
    {
        public static readonly string CpfRequired = "O CPF é obrigatório.";
    }
}