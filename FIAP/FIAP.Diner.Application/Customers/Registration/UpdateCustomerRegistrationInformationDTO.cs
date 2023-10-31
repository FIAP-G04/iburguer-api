namespace FIAP.Diner.Application.Customers.Registration;

public record UpdateCustomerRegistrationInformationDTO(Guid customerId, string firstName,
    string lastName, string email);