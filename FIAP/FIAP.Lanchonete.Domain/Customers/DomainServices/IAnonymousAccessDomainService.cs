namespace FIAP.Diner.Domain.Customers.DomainServices
{
    public interface IAnonymousAccessDomainService
    {
        Task<Customer> RegisterAnonymousCustomer();
    }
}
