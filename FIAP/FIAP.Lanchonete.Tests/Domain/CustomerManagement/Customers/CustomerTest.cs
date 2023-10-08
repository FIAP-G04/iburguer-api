using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.CustomerManagement.Customers;
using FluentAssertions;

namespace FIAP.Diner.Tests.Domain.CustomerManagement.Customers
{
    public class CustomerTest
    {
        [Fact]
        public void ShouldCreateCustomer()
        {
            //Arrange
            var registration = DateTime.Now;
            var cpf = "12345678909";
            var name = FullName.From("Customer","Name");
            var email = "customer.email@fiap.com";

            //Act
            var customer = new Customer(cpf, name, email);

            //Assert
            customer.Id.Should().NotBeNull();
            customer.CPF.Should().Be(cpf);
            customer.Name.Should().Be(name);
            customer.Email.Should().Be(email);
            customer.RegistrationDate.Should().BeAfter(registration);
            customer.LastUpdated.Should().Be(customer.RegistrationDate);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCPFNotProvided()
        {
            //Arrange
            var name = FullName.From("Customer","Name");
            var email = "customer.email@fiap.com";

            //Act
            var action = () => new Customer(null, name, email);

            //Assert
            action.Should().Throw<DomainException>()
                .WithMessage(CPF.Errors.CpfRequired);
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameNotProvided()
        {
            //Arrange
            var cpf = "12345678909";
            var email = "customer.email@fiap.com";

            //Act
            var action = () => new Customer(cpf, null, email);

            //assert
            action.Should().Throw<DomainException>()
                .WithMessage(FullName.Errors.NameRequired);
        }

        [Fact]
        public void ShouldChangeCustomer()
        {
            //Arrange
            var cpf = "12345678909";
            var registration = DateTime.Now;
            var name = FullName.From("Name","Changed");
            var email = "customer@changed.com";

            var customer = new Customer(cpf, FullName.From("Old","Name"), "customer@old.com");

            //Act
            customer.Change(name, email);

            //Assert
            customer.Id.Should().NotBeNull();
            customer.CPF.Should().Be(cpf);
            customer.Name.Should().Be(name);
            customer.Email.Should().Be(email);
            customer.RegistrationDate.Should().BeAfter(registration);
            customer.LastUpdated.Should().BeAfter(customer.RegistrationDate);
        }
    }
}
