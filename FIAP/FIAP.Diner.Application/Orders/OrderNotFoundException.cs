using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Orders;

public class OrderNotFoundException : DomainException
{
    public const string error = "Não existe nenhum pedido cadastro com o Id {0}";

    public OrderNotFoundException(Guid orderId) : base(
        string.Format(error, orderId.ToString()))
    {
    }
}