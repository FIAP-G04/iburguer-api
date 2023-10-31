using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Customers;

namespace FIAP.Diner.Tests.Domain.CustomerManagement.Customers;

public class EmailTest
{
    [Theory]
    [InlineData("user@example.com")]
    [InlineData("john.doe123@gmail.com")]
    [InlineData("jane_doe@example.co.uk")]
    [InlineData("user1234@sub.domain.info")]
    [InlineData("user.name@my-website.net")]
    [InlineData("contact@company-name.com")]
    [InlineData("admin@12345.co")]
    [InlineData("info@my-website.io")]
    [InlineData("support@website.co.jp")]
    public void ShouldValidEmail(string email) =>
        //Act & Assert
        new Email(email).Value.Should().Be(email);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ShouldThrowErrorWhenEmailNotProvided(string email)
    {
        //Act
        var action = () => new Email(email);

        //Assert
        action.Should().Throw<DomainException>()
            .WithMessage(Email.Errors.EmailRequired);
    }

    [Theory]
    [InlineData("user@domain")]
    [InlineData("@example.com")]
    [InlineData("user@.com")]
    [InlineData("user@domain.")]
    [InlineData("user@example")]
    [InlineData("@.")]
    [InlineData("user@-example.com")]
    [InlineData("user@ex_ample.com")]
    [InlineData("user@example..com")]
    [InlineData("user@example.c")]
    [InlineData("1")]
    [InlineData("user")]
    public void ShouldThrowErrorWhenEmailInvalid(string email)
    {
        //Act
        var action = () => new Email(email);

        //Assert
        action.Should().Throw<DomainException>()
            .WithMessage(Email.Errors.InvalidEmail);
    }
}