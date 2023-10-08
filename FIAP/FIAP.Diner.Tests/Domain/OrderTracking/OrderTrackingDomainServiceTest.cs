using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Tracking;

namespace FIAP.Diner.Tests.Domain.OrderTracking;

public class OrderTrackingDomainServiceTest
{
    private readonly IOrderRepository _orderRepository;

    private readonly OrderTrackingDomainService _manipulator;

    public OrderTrackingDomainServiceTest()
    {
        _orderRepository = Substitute.For<IOrderRepository>();

        _manipulator = new(_orderRepository);
    }

    [Fact]
    public async Task ShouldRegisterOrderTracking()
    {
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        await _manipulator.RegisterOrderTracking(orderId, customerId);

        await _orderRepository.Received()
            .Save(Arg.Is<Diner.Domain.Tracking.OrderTracking>(o =>
                o.OrderId == orderId &&
                o.CustomerId == customerId));
    }

    [Fact]
    public async Task ShouldUpdateOrderTrackingStatus()
    {
        var order = new Diner.Domain.Tracking.OrderTracking(Guid.NewGuid(), Guid.NewGuid());

        _orderRepository.GetByOrderId(order.OrderId).Returns(order);

        await _manipulator.UpdateOrderTracking(order.OrderId, OrderStatus.Ready);

        await _orderRepository.Received()
            .Update(Arg.Is<Diner.Domain.Tracking.OrderTracking>(o =>
                o.Id == order.Id &&
                o.Status.OrderStatus == OrderStatus.Ready));
    }

    [Fact]
    public async Task ShouldThrowErrorWhenOrderDoesNotExist()
    {
        _orderRepository.GetByOrderId(Arg.Any<Guid>()).ReturnsNull();

        var action = async () =>
            await _manipulator.UpdateOrderTracking(Guid.NewGuid(), OrderStatus.Finished);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(OrderTrackingExceptions.OrderNotFound);
    }
}