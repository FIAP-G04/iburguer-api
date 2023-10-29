namespace FIAP.Diner.Application.Customers.Identification;

public interface ICustomerIdentifier
{
    Task<IdentifiedCustomerDTO> Indentify(string cpf, CancellationToken cancellation);
}