namespace FIAP.Diner.Domain.Customers.DomainServices
{
    public class RegisterCustomerDomainService : IRegisterCustomerDomainService
    {
        private readonly ICustomerRepository _customerRepository;

        public RegisterCustomerDomainService(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<Customer> RegisterCustomer(string cpf, string email, string name)
        {
            var customerEmail = new Email(email);

            var customer = new Customer(cpf, name, customerEmail);

            await _customerRepository.Register(customer);

            return customer;
        }
    }
}
