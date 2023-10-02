namespace FIAP.Diner.Tests.Domain.Customers;

public class RegisterCustomerDomainServiceTest
{
    [Fact]
    public async Task ShouldRegisterCustomer()
    {
        var customerRepository = Substitute.For<ICustomerRepository>();

        var manipulator = new RegisterCustomerDomainService(customerRepository);

        const string cpf = "11111111111";
        const string name = "Customer Name";
        const string email = "customer.email@fiap.com";

        var customer = await manipulator.RegisterCustomer(cpf, email, name);

        await customerRepository
            .Received()
            .Register(Arg.Is<Customer>(c =>
                c.Email != null &&
                c.CPF == cpf &&
                c.Name == name &&
                c.Email.Value == email));

        customer.CPF.Should().Be(cpf);
        customer.Email?.Value.Should().Be(email);
        customer.Name.Should().Be(name);
    }
}