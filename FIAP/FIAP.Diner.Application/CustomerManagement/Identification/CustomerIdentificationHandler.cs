using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.CustomerManagement.Customers;

namespace FIAP.Diner.Application.CustomerManagement.Identification;

public class CustomerIdentificationHandler : IQueryHandler<IdentifyCustomerQuery, IdentifiedCustomer>
{
    private readonly ICustomerRepository _repository;

    public CustomerIdentificationHandler(ICustomerRepository customerRepository)
    {
        ArgumentNullException.ThrowIfNull(customerRepository);

        _repository = customerRepository;
    }

    public async Task<IdentifiedCustomer> Handle(IdentifyCustomerQuery query, CancellationToken cancellation)
    {
        var customer = await _repository.GetByCpf(query.cpf, cancellation);

        if (customer is null)
        {
            throw new UnidentifiedCustomerException(query.cpf);
        }

        return new IdentifiedCustomer(customer.Id, customer.Name.ToString());
    }
}