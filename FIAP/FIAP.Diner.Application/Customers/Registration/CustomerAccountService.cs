using FIAP.Diner.Domain.Customers;

namespace FIAP.Diner.Application.Customers.Registration;

public class CustomerAccountService : ICustomerAccount
{
    private readonly ICustomerRepository _repository;

    public CustomerAccountService(ICustomerRepository customerRepository)
    {
        ArgumentNullException.ThrowIfNull(customerRepository);

        _repository = customerRepository;
    }

    public async Task RegisterCustomer(RegisterCustomerDTO command, CancellationToken cancellation)
    {
        var customer = new Customer(command.cpf,
            PersonName.From(command.firstName, command.lastName),
            command.email);

        await _repository.Register(customer, cancellation);
    }

    public async Task UpdateCustomerRegistrationInformation(UpdateCustomerRegistrationInformationDTO command,
        CancellationToken cancellation)
    {
        var customer = await _repository.GetById(command.customerId, cancellation);

        customer.Change(PersonName.From(command.firstName, command.lastName), command.email);

        await _repository.UpdateCustomerRegistration(customer, cancellation);
    }
}