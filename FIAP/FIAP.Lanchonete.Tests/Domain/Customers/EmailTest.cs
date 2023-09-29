using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.Customers;
using FluentAssertions;

namespace FIAP.Diner.Tests.Domain.Customers
{
    public class EmailTest
    {
        [Fact]
        public void ShouldCreateEmail()
        {
            var emailValue = "valid.email@fiap.com";

            var email = new Email(emailValue);

            email.Value.Should().Be(emailValue);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowErrorWhenEmailNotProvided(string emailValue)
        {
            var action = () => new Email(emailValue);

            action.Should().Throw<DomainException>()
                .WithMessage(CustomerExceptions.EmailRequired);
        }

        [Fact]
        public void ShouldThrowErrorWhenEmailInvalid()
        {
            var emailValue = "abc123";

            var action = () => new Email(emailValue);

            action.Should().Throw<DomainException>()
                .WithMessage(CustomerExceptions.InvalidEmail);
        }
    }
}
