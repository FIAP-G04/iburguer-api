namespace FIAP.Diner.Application.Customers.Identification;

public interface IIdentifyCustomerUseCase
{
    Task<IdentifiedCustomerDTO> Indentify(string cpf, CancellationToken cancellation);
}