using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Orders;

public class OrderTracking : Entity<OrderTrackingId>
{
    public OrderStatus OrderStatus { get; private set; }
    public DateTime When { get; private set; }
    public OrderId Order { get; private set; }

    private OrderTracking() { }

    public OrderTracking(OrderId order, OrderStatus orderStatus)
    {
        Id = OrderTrackingId.New;
        OrderStatus = orderStatus;
        When = DateTime.Now;
        Order = order;
    }
}