using FIAP.Diner.Domain.Customers;

namespace FIAP.Diner.Application.Customers.Registration
{
    public interface IUpdateCustomerRegistrationInformationUseCase
    {
        Task UpdateCustomerRegistrationInformation(UpdateCustomerRegistrationInformationDTO dto, CancellationToken cancellation);
    }

    public class UpdateCustomerRegistrationInformationUseCase : IUpdateCustomerRegistrationInformationUseCase
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerRegistrationInformationUseCase(ICustomerRepository repository) => _repository = repository;

        public async Task UpdateCustomerRegistrationInformation(UpdateCustomerRegistrationInformationDTO dto,
        CancellationToken cancellation)
        {
            var customer = await _repository.GetById(dto.customerId, cancellation);

            customer.Change(PersonName.From(dto.firstName, dto.lastName), dto.email);

            await _repository.UpdateCustomerRegistration(customer, cancellation);
        }
    }
}
