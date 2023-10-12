using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Tracking;

public class TrackingStatus : Entity<TrackingStatusId>
{
    public OrderStatus OrderStatus { get; private set; }
    public DateTime DateTime { get; }

    public TrackingStatus(OrderStatus orderStatus)
    {
        Id = TrackingStatusId.New;
        OrderStatus = orderStatus;
        DateTime = DateTime.Now;
    }
}