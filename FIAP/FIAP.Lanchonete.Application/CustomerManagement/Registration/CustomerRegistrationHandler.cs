using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.CustomerManagement.Customers;

namespace FIAP.Diner.Application.CustomerManagement.Registration
{
    public class CustomerRegistrationHandler : ICommandHandler<RegisterCustomerCommand>,
                                               ICommandHandler<UpdateCustomerRegistrationInformationCommand>
    {
        private readonly ICustomerRepository _repository;

        public CustomerRegistrationHandler(ICustomerRepository customerRepository)
        {
            ArgumentNullException.ThrowIfNull(customerRepository);

            _repository = customerRepository;
        }

        public Task Handle(RegisterCustomerCommand command, CancellationToken cancellation)
        {
            var customer = new Customer(command.cpf,
                                        FullName.From(command.firstName, command.lastName),
                                      command.email);

            return _repository.Register(customer, cancellation);
        }

        public async Task Handle(UpdateCustomerRegistrationInformationCommand command,
            CancellationToken cancellation)
        {
            var customer = await _repository.GetById(command.customerId, cancellation);

            customer.Change(FullName.From(command.firstName, command.lastName), command.email);

            await _repository.UpdateCustomerRegistration(customer, cancellation);
        }
    }
}
