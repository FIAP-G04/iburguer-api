using FIAP.Diner.Domain.Customers;
using FIAP.Diner.Domain.Customers.DomainServices;
using FluentAssertions;
using NSubstitute;

namespace FIAP.Diner.Tests.Domain.Customers
{
    public class AnonymousAccessDomainServiceTest
    {
        [Fact]
        public async void ShouldRegisterCustomer()
        {
            var customerRepository = Substitute.For<ICustomerRepository>();

            var manipulator = new AnonymousAccessDomainService(customerRepository);

            var customer = await manipulator.RegisterAnonymousCustomer();

            await customerRepository.Received().Register(Arg.Any<Customer>());

            customer.Should().NotBeNull();
            customer.Type.Should().Be(CustomerType.Anonymous);
        }
    }
}
