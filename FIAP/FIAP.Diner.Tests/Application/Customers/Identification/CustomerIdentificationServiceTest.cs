using Castle.Core.Resource;
using FIAP.Diner.Application.Customers.Identification;
using FIAP.Diner.Domain.Customers;

namespace FIAP.Diner.Tests.Application.Customers.Identification
{
    public class CustomerIdentificationServiceTest
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly CustomerIdentificationService _manipulator;

        public CustomerIdentificationServiceTest()
        {
            _customerRepository = Substitute.For<ICustomerRepository>();

            _manipulator = new(_customerRepository);
        }

        [Fact]
        public async Task ShouldIdentifyCustomer()
        {
            var customer = new Customer("92707684066", PersonName.From("First", "Last"), new Email("abc@def.com"));

            _customerRepository.GetByCpf(customer.CPF, Arg.Any<CancellationToken>()).Returns(customer);

            var result = await _manipulator.Indentify(customer.CPF, default);

            result.id.Should().Be(customer.Id.Value);
            result.name.Should().Be(customer.Name.ToString());
            result.cpf.Should().Be(customer.CPF);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenCustomerUnidentified()
        {
            var cpf = "92707684066";

            _customerRepository.GetByCpf(cpf, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _manipulator.Indentify(cpf, default);

            await action.Should().ThrowAsync<UnidentifiedCustomerException>()
                .WithMessage(string.Format(UnidentifiedCustomerException.error, cpf));
        }
    }
}
