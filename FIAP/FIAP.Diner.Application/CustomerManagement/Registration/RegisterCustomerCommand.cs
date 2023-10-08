namespace FIAP.Diner.Application.CustomerManagement.Registration;

public record RegisterCustomerCommand(string cpf, string firstName, string lastName, string email);