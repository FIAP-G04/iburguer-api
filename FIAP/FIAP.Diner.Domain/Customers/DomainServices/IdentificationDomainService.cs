using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Customers.DomainServices
{
    public class IdentificationDomainService : IIdentificationDomainService
    {
        private readonly ICustomerRepository _customerRepository;

        public IdentificationDomainService(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<Customer> IdentifyCustomer(string cpf)
        {
            var customer = await _customerRepository.Get(cpf) ?? throw new DomainException(CustomerExceptions.CustomerWithCPFDoesNotExist);

            return customer;
        }
    }
}
