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

    public async Task RegisterCustomer(RegisterCustomerDTO dto, CancellationToken cancellation)
    {
        var customer = new Customer(dto.cpf, PersonName.From(dto.firstName, dto.lastName), dto.email);

        await _repository.Register(customer, cancellation);
    }

    public async Task UpdateCustomerRegistrationInformation(UpdateCustomerRegistrationInformationDTO dto,
        CancellationToken cancellation)
    {
        var customer = await _repository.GetById(dto.customerId, cancellation);

        customer.Change(PersonName.From(dto.firstName, dto.lastName), dto.email);

        await _repository.UpdateCustomerRegistration(customer, cancellation);
    }
}