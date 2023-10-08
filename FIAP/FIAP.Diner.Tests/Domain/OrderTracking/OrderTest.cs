using FIAP.Diner.Domain.Tracking;

namespace FIAP.Diner.Tests.Domain.OrderTracking;

public class OrderTest
{
    [Fact]
    public void ShouldCreateOrder()
    {
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var order = new Diner.Domain.Tracking.OrderTracking(orderId, customerId);

        order.OrderId.Should().Be(orderId);
        order.CustomerId.Should().Be(customerId);

        order.Status.OrderStatus.Should().Be(OrderStatus.WaitingForPayment);
    }

    [Fact]
    public void ShouldUpdateOrderStatus()
    {
        var order = new Diner.Domain.Tracking.OrderTracking(Guid.NewGuid(), Guid.NewGuid());

        order.UpdateStatus(OrderStatus.Received);

        order.Status.OrderStatus.Should().Be(OrderStatus.Received);

        var raisedEvent = order.Events.First(e =>
            e.GetType().Equals(typeof(OrderStatusUpdatedDomainEvent))) as OrderStatusUpdatedDomainEvent;
        raisedEvent.OrderId.Should().Be(order.OrderId);
        raisedEvent.CustomerId.Should().Be(order.CustomerId);
        raisedEvent.Status.Should().Be(order.Status);
    }
}