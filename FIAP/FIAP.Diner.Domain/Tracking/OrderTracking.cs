using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Tracking;

public class OrderTracking : Entity<OrderTrackingId>, IAggregateRoot
{
    private IList<TrackingStatus> _statusHistory { get; }

    public IReadOnlyCollection<TrackingStatus> StatusHistory =>
        _statusHistory.AsReadOnly();

    public TrackingStatus Status => _statusHistory.OrderByDescending(s => s.DateTime).FirstOrDefault();
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }

    public OrderTracking(Guid orderId, Guid customerId)
    {
        Id = Guid.NewGuid();

        OrderId = orderId;
        CustomerId = customerId;

        _statusHistory = new List<TrackingStatus>();
        _statusHistory.Add(new TrackingStatus(OrderStatus.WaitingForPayment));
    }

    public void UpdateStatus(OrderStatus orderStatus)
    {
        _statusHistory.Add(new TrackingStatus(orderStatus));
        RaiseEvent(new OrderStatusUpdatedDomainEvent(OrderId, CustomerId, Status));
    }
}