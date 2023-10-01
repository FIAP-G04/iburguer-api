using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Onboarding;

public struct CPF
{
    public static int CheckDigitsLength = 2;
    public static int NumericalDigitsLength = 9;

    private string _numericalDigits;
    private string _checkDigits;

    public static int CpfLength => CheckDigitsLength + NumericalDigitsLength;

    public string NumericalDigits => _numericalDigits;

    public string CheckDigits => _checkDigits;

    public string Number => _numericalDigits + _checkDigits;

    public override string ToString() => Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");

    public static implicit operator string(CPF value) => value.Number;

    public static implicit operator CPF(string value) => new CPF(value);

    public CPF(string number)
    {
        Validate(number);

        _numericalDigits = number.Substring(0, NumericalDigitsLength);
        _checkDigits = number.Substring(NumericalDigitsLength, CheckDigitsLength);
    }

    private void Validate(string number)
    {
        if (number is null || number == string.Empty)
        {
            throw new DomainException(Errors.CpfRequired);
        }

        if (!(IsNumeric(number) && IsValidLegth(number)))
        {
            throw new DomainException(Errors.InvalidDigitAndSize);
        }

        if (AllDigitsAreEqual(number) || IsValidCheckDigits(number))
        {
            throw new DomainException(Errors.InvalidCpf);
        }
    }

    private bool AllDigitsAreEqual(string value) => value.All(digit => digit == value[0]);

    private bool IsNumeric(string value) => value.All(char.IsNumber);

    private bool IsValidLegth(string value) => value.Length == CpfLength;

    private bool IsValidCheckDigits(string cpf)
    {
        var numericalDigits = cpf.Substring(0,9).ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

        var checkDigits = CalculateCheckDigits(numericalDigits);

        return !cpf.Substring(NumericalDigitsLength, CheckDigitsLength).Equals(checkDigits);
    }

    private string CalculateCheckDigits(int[] numbers)
    {
        var sum = 0;

        for (var i = 0; i < numbers.Length; i++)
        {
            sum += (numbers[i] * (i + 1));
        }

        var firstDigit = GetDigitFromRemainder(sum % CpfLength);

        sum = 0;

        var numbersPlusFirstCheckDigit = numbers.Append(firstDigit).ToArray();

        for (var i = 0; i < numbersPlusFirstCheckDigit.Length; i++)
        {
            sum += (numbersPlusFirstCheckDigit[i] * i);
        }

        var secondDigit = GetDigitFromRemainder(sum % CpfLength);

        return $"{firstDigit}{secondDigit}";
    }

    private int GetDigitFromRemainder(int remainder) => remainder >= 10 ? default : remainder;

    public static class Errors
    {
        public static readonly string InvalidCpf = "A cadeia de caracteres informada não corresponde a um CPF válido.";
        public static readonly string InvalidDigitAndSize = $"Era esperado uma string numérica de {CpfLength} dígitos.";
        public static readonly string CpfRequired = "Não é possível criar um CPF a partir de um valor nulo.";
    }
}