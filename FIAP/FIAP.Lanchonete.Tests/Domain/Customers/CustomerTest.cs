using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.Customers;
using FIAP.Diner.Domain.Onboarding;
using FluentAssertions;

namespace FIAP.Diner.Tests.Domain.Customers
{
    public class CustomerTest
    {
        [Fact]
        public void ShouldCreateAnonymousUser()
        {
            var customer = new Customer();

            customer.Type.Should().Be(CustomerType.Anonymous);
            customer.Id.Should().NotBeEmpty();
            customer.CPF.Should().BeNull();
            customer.Name.Should().BeNull();
            customer.Email.Should().BeNull();
        }

        [Fact]
        public void ShouldCreateIdentifiedUser()
        {
            var cpf = "11111111111";
            var name = "Customer Name";
            var email = new Email("customer.email@fiap.com");

            var customer = new Customer(cpf, name, email);

            customer.Type.Should().Be(CustomerType.Identified);
            customer.Id.Should().NotBeEmpty();
            customer.CPF.Should().Be(cpf);
            customer.Name.Should().Be(name);
            customer.Email.Should().Be(email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenCPFNotProvided(string cpf)
        {
            var name = "Customer Name";
            var email = new Email("customer.email@fiap.com");

            var action = () => new Customer(cpf, name, email);

            action.Should().Throw<DomainException>()
                .WithMessage(CustomerExceptions.CpfRequired);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenNameNotProvided(string name)
        {
            var cpf = "11111111111";
            var email = new Email("customer.email@fiap.com");

            var action = () => new Customer(cpf, name, email);

            action.Should().Throw<DomainException>()
                .WithMessage(CustomerExceptions.NameRequired);
        }
    }
}
