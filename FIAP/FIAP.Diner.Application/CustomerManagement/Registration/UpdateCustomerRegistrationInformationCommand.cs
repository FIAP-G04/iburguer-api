namespace FIAP.Diner.Application.CustomerManagement.Registration;

public record UpdateCustomerRegistrationInformationCommand(Guid customerId, string firstName, string lastName, string email);