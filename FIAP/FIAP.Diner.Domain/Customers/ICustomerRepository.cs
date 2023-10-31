using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Customers;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCpf(CPF cpf, CancellationToken cancellation);

    Task<Customer?> GetById(CustomerId id, CancellationToken cancellation);

    Task Register(Customer customer, CancellationToken cancellation);

    Task UpdateCustomerRegistration(Customer customer, CancellationToken cancellation);
}