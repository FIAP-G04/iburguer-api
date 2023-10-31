namespace FIAP.Diner.Application.Customers.Registration;

public interface ICustomerAccount
{
    Task RegisterCustomer(RegisterCustomerDTO dto, CancellationToken cancellation);

    Task UpdateCustomerRegistrationInformation(UpdateCustomerRegistrationInformationDTO dto,
        CancellationToken cancellation);
}