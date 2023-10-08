using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Tracking;

public class OrderTracking : Entity<OrderTrackingId>, IAggregateRoot
{
    public TrackingStatus Status { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }

    public OrderTracking(Guid orderId, Guid customerId)
    {
        Id = new OrderTrackingId();

        OrderId = orderId;
        CustomerId = customerId;

        Status = new TrackingStatus(OrderStatus.WaitingForPayment);
    }

    public void UpdateStatus(OrderStatus orderStatus)
    {
        Status = new TrackingStatus(orderStatus);

        RaiseEvent(new OrderStatusUpdatedDomainEvent(OrderId, CustomerId, Status));
    }
}