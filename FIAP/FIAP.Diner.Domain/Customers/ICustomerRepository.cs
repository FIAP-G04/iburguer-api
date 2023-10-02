using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> Get(string cpf);

        Task Register(Customer customer);
    }
}
