namespace FIAP.Diner.Domain.Customers.DomainServices
{
    public interface IIdentificationDomainService
    {
        Task<Customer> IdentifyCustomer(string cpf);
    }
}
