using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Tests.Domain.Order;

public class OrderTest
{
    [Fact]
    public void ShouldCreateOrder()
    {
        var cartId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var order = new Diner.Domain.Order.Order(cartId, customerId);

        order.Id.Should().NotBeNull();
        order.Id.Value.Should().NotBe(Guid.Empty);
        order.CartId.Value.Should().Be(cartId);
        order.CustomerId.Value.Should().Be(customerId);

        order.Status.OrderStatus.Should().Be(OrderStatus.WaitingForPayment);
    }

    [Fact]
    public void ShouldUpdateOrderStatus()
    {
        var order = new Diner.Domain.Order.Order(Guid.NewGuid(), Guid.NewGuid());

        order.UpdateStatus(OrderStatus.Received);

        order.Status.OrderStatus.Should().Be(OrderStatus.Received);

        var raisedEvent = order.Events.First(e =>
            e.GetType().Equals(typeof(OrderStatusUpdatedDomainEvent))) as OrderStatusUpdatedDomainEvent;
        raisedEvent!.OrderId.Should().Be(order.Id);
        raisedEvent!.CustomerId.Should().Be(order.CustomerId);
        raisedEvent!.Status.Should().Be(order.Status);
    }
}