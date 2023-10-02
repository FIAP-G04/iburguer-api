namespace FIAP.Diner.Domain.Customers.DomainServices
{
    public interface IRegisterCustomerDomainService
    {
        Task<Customer> RegisterCustomer(string cpf, string email, string name);
    }
}
