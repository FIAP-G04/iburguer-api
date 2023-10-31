using FIAP.Diner.Application.Customers.Registration;
using FIAP.Diner.Domain.Customers;

namespace FIAP.Diner.Tests.Application.CustomerManagement.Registration;

public class CustomerRegistrationHandlerTest
{
    private readonly ICustomerRepository _repository;
    private readonly CustomerAccountService _sut;

    public CustomerRegistrationHandlerTest()
    {
        _repository = Substitute.For<ICustomerRepository>();
        _sut = new CustomerAccountService(_repository);
    }

    [Fact]
    public async Task ShouldRegisterCustomer()
    {
        //Arrange
        var cmd = new RegisterCustomerCommand("12345678909", "Customer", "Name",
            "customer@fiap.com");

        //Act
        await _sut.Handle(cmd, CancellationToken.None);

        await _repository
            .Received(1)
            .Register(Arg.Is<Customer>(c =>
                c.CPF == cmd.cpf &&
                c.Name.ToString().Equals($"{cmd.firstName} {cmd.lastName}") &&
                c.Email == cmd.email), CancellationToken.None);
    }

    [Fact]
    public async Task ShouldUpdateCustomerRegistrationInformation()
    {
        //Arrange
        var cpf = "12345678909";

        var customer = new Customer(cpf, PersonName.From("Old", "Name"), "customer@old.com");

        _repository.GetById(customer.Id, CancellationToken.None).Returns(customer);

        var cmd = new UpdateCustomerRegistrationInformationCommand(customer.Id, "Name", "Changed",
            "customer@changed.com");

        //Act
        await _sut.Handle(cmd, CancellationToken.None);

        await _repository
            .Received(1)
            .UpdateCustomerRegistration(Arg.Is<Customer>(c =>
                c.CPF == cpf &&
                c.Name.ToString().Equals($"{cmd.firstName} {cmd.lastName}") &&
                c.Email == cmd.email), CancellationToken.None);
    }
}