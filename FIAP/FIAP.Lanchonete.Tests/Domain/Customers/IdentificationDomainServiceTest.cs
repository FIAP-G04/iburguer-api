using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.Customers;
using FIAP.Diner.Domain.Customers.DomainServices;
using FluentAssertions;
using NSubstitute;

namespace FIAP.Diner.Tests.Domain.Customers
{
    public class IdentificationDomainServiceTest
    {
        private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();

        private readonly IdentificationDomainService _manipulator;

        public IdentificationDomainServiceTest()
        {
            _manipulator = new(_customerRepository);
        }

        [Fact]
        public async Task ShouldIdentifyCustomer()
        {
            var cpf = "1111111111";
            var customer = new Customer(cpf, "Customer Name", new Email("email@fiap.com"));

            _customerRepository.Get(cpf).Returns(customer);

            var result = await _manipulator.IdentifyCustomer(cpf);

            result.CPF.Should().Be(cpf);
            result.Email.Should().Be(customer.Email);
            result.Name.Should().Be(customer.Name);
            result.Type.Should().Be(CustomerType.Identified);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenCpfNotIdentified()
        {
            var cpf = "1111111111";

            var action = async () => await _manipulator.IdentifyCustomer(cpf);

            await action.Should().ThrowAsync<DomainException>()
                .WithMessage(CustomerExceptions.CustomerWithCPFDoesNotExist);
        }
    }
}
