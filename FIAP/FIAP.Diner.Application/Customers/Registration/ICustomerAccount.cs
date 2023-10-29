namespace FIAP.Diner.Application.Customers.Registration;

public interface ICustomerAccount
{
    Task RegisterCustomer(RegisterCustomerDTO command, CancellationToken cancellation);

    Task UpdateCustomerRegistrationInformation(UpdateCustomerRegistrationInformationDTO command,
        CancellationToken cancellation);
}