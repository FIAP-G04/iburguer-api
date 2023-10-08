namespace FIAP.Diner.Domain.Tracking;

public interface IOrderTrackingDomainService
{
    Task RegisterOrderTracking(Guid orderId, Guid customerId);

    Task UpdateOrderTracking(Guid orderId, OrderStatus orderStatus);
}