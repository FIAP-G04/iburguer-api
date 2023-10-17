using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.OrderTracking.Tracking;

public class OrderTrackingNotFoundException : DomainException
{
    public const string error = "Não existe rastreio para o pedido cadastrado com o id {0}";

    public OrderTrackingNotFoundException(Guid orderTrackingId) : base(string.Format(error, orderTrackingId.ToString()))
    {

    }
}