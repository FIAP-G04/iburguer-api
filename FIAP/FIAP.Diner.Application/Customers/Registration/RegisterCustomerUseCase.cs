using FIAP.Diner.Domain.Customers;

namespace FIAP.Diner.Application.Customers.Registration
{
    public interface IRegisterCustomerUseCase
    {
        Task RegisterCustomer(RegisterCustomerDTO dto, CancellationToken cancellation);
    }

    public class RegisterCustomerUseCase : IRegisterCustomerUseCase
    {
        private readonly ICustomerRepository _repository;

        public RegisterCustomerUseCase(ICustomerRepository repository) => _repository = repository;

        public async Task RegisterCustomer(RegisterCustomerDTO dto, CancellationToken cancellation)
        {
            var customer = new Customer(dto.cpf, PersonName.From(dto.firstName, dto.lastName), dto.email);

            await _repository.Register(customer, cancellation);
        }
    }
}
