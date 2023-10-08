using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.CustomerManagement.Customers;
using FluentAssertions;

namespace FIAP.Diner.Tests.Domain.CustomerManagement.Customers;

public class CPFTest
{
    [Theory]
    [InlineData("456")]
    [InlineData("4561239949020202")]
    [InlineData("cpfcpfcpfab")]
    [InlineData("1@%&*$/?!-+")]
    [InlineData("11111@11111")]
    public void ShouldThrowErrorWhenCpfWithIsInvalidDigitOrLength(string cpf)
    {
        //Act
        var action = () => new CPF(cpf);

        //Assert
        action.Should().Throw<DomainException>().WithMessage(CPF.Errors.InvalidDigitAndSize);
    }

    [Theory]
    [InlineData("11111111111")]
    [InlineData("11011011045")]
    [InlineData("12345678999")]
    [InlineData("45609899100")]
    public void ShouldThrowErrorWhenCpfIsInvalid(string cpf)
    {
        //Act
        var action = () => new CPF(cpf);

        //Assert
        action.Should().Throw<DomainException>().WithMessage(CPF.Errors.InvalidCpf);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowErrorWhenCpfIsEmptyOrNull(string cpf)
    {
        //Act
        var action = () => new CPF(cpf);

        //Assert
        action.Should().Throw<DomainException>().WithMessage(CPF.Errors.CpfRequired);
    }

    [Theory]
    [InlineData("46793281771")]
    [InlineData("38599736817")]
    [InlineData("16274374019")]
    [InlineData("00642644675")]
    [InlineData("52523249219")]
    [InlineData("31564007502")]
    [InlineData("22028088508")]
    [InlineData("84841093303")]
    [InlineData("50581385900")]
    [InlineData("26431778430")]
    [InlineData("12345678909")]
    [InlineData("11066001774")]
    [InlineData("29414805010")]
    public void ShouldCheckTheCpfNumberSuccessfully(string cpf) => new CPF(cpf).Number.Should().Be(cpf);

    [Fact]
    public void ShouldDisplayTheCpfNumberWithFormatting()
    {
        //Arrange
        CPF cpf = "46793281771";

        //Act & Assert
        cpf.ToString().Should().Be("467.932.817-71");
    }

    [Fact]
    public void ShouldDisplayTheNumericalDigitsFromCpf()
    {
        //Arrange
        CPF cpf = "46793281771";

        //Act & Assert
        cpf.NumericalDigits.Should().Be("467932817");
    }

    [Fact]
    public void ShouldDisplayTheCheckDigitsFromCpf()
    {
        //Arrange
        CPF cpf = "46793281771";

        //Act & Assert
        cpf.CheckDigits.Should().Be("71");
    }
}