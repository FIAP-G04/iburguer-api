namespace FIAP.Diner.Domain.Customers.DomainServices
{
    public class AnonymousAccessDomainService : IAnonymousAccessDomainService
    {
        private readonly ICustomerRepository _customerRepository;

        public AnonymousAccessDomainService(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<Customer> RegisterAnonymousCustomer()
        {
            var customer = new Customer();

            await _customerRepository.Register(customer);

            return customer;
        }
    }
}
