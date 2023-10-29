using FIAP.Diner.Domain.Customers;

namespace FIAP.Diner.Application.Customers.Identification;

public class CustomerIdentificationService : ICustomerIdentifier
{
    private readonly ICustomerRepository _repository;

    public CustomerIdentificationService(ICustomerRepository customerRepository)
    {
        ArgumentNullException.ThrowIfNull(customerRepository);

        _repository = customerRepository;
    }

    public async Task<IdentifiedCustomerDTO> Indentify(string cpf, CancellationToken cancellation)
    {
        var customer = await _repository.GetByCpf(cpf, cancellation);

        if (customer is null) throw new UnidentifiedCustomerException(cpf);

        return new IdentifiedCustomerDTO(customer.Id, customer.Name.ToString(), customer.CPF);
    }
}