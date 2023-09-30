namespace FIAP.Diner.Domain.Onboarding;

public struct CPF
{
    private const int checkDigitsLength = 2;
    private const int numericalDigitsLength = 9;
    private const int cpfLength = checkDigitsLength + numericalDigitsLength;

    private string _numericalDigits;
    private string _checkDigits;

    public string NumericalDigits => _numericalDigits;

    public string CheckDigits => _checkDigits;

    public string Number => _numericalDigits + _checkDigits;

    public override string ToString() => Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");

    public static implicit operator string(CPF value) => value.Number;

    public static implicit operator CPF(string value) => new CPF(value);

    public CPF(string number)
    {
        Validate(number);

        _numericalDigits = number.Substring(0, numericalDigitsLength);
        _checkDigits = number.Substring(numericalDigitsLength, checkDigitsLength);
    }

    private void Validate(string number)
    {
        if (number is null || number == string.Empty)
        {
            throw new ArgumentNullException(nameof(number),
                "Não é possível criar um CPF a partir de um valor nulo.");
        }

        if (AllDigitsAreEqual(number))
        {
            throw new InvalidOperationException(
                "A cadeia de caracteres informada não corresponde a um CPF válido.");
        }

        if (!(IsNumeric(number) && IsValidLegth(number)))
        {
            throw new ArgumentOutOfRangeException(
                $"Era esperado uma string numérica de {cpfLength} dígitos", nameof(number));
        }

        int firstDigit = CalculateCheckDigit(number, 10);
        int secondDigit = CalculateCheckDigit(number, 11);

        if (number[9] - '0' != firstDigit || number[10] - '0' != secondDigit)
        {
            throw new InvalidOperationException(
                "A cadeia de caracteres informada não corresponde a um CPF válido.");
        }
    }

    private bool AllDigitsAreEqual(string value) => value.All(digit => digit == value[0]);

    private bool IsNumeric(string value) => value.All(char.IsNumber);

    private bool IsValidLegth(string value) => value.Length == cpfLength;

    private int CalculateCheckDigit(string cpf, int weight)
    {
        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += (cpf[i] - '0') * weight;
            weight--;
        }

        int remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }
}