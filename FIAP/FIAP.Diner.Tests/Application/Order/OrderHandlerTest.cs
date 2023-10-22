using FIAP.Diner.Application.Order.Tracking;
using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Tests.Application.Order;

public class OrderHandlerTest
{
    private readonly IOrderRepository orderRepository;

    private readonly OrderHandler _manipulator;

    public OrderHandlerTest()
    {
        orderRepository = Substitute.For<IOrderRepository>();

        _manipulator = new(orderRepository);
    }

    [Fact]
    public async Task ShouldRegisterOrderTracking()
    {
        var cartId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var command = new RegisterOrderCommand(cartId, customerId);

        await _manipulator.Handle(command, default);

        await orderRepository.Received()
            .Save(Arg.Is<Diner.Domain.Order.Order>(o =>
                o.CartId.Value == cartId &&
                o.CustomerId.Value == customerId));
    }

    [Fact]
    public async Task ShouldUpdateOrderTrackingStatus()
    {
        var orderTracking = new Diner.Domain.Order.Order(Guid.NewGuid(), Guid.NewGuid());

        var command = new UpdateOrderTrackingCommand(orderTracking.Id, OrderStatus.Ready);

        orderRepository.Get(orderTracking.Id).Returns(orderTracking);

        await _manipulator.Handle(command, default);

        await orderRepository.Received()
            .Update(Arg.Is<Diner.Domain.Order.Order>(o =>
                o.Id == orderTracking.Id &&
                o.Status.OrderStatus == command.OrderStatus));
    }

    [Fact]
    public async Task ShouldThrowErrorWhenOrderDoesNotExist()
    {
        orderRepository.Get(Arg.Any<Guid>()).ReturnsNull();

        var command = new UpdateOrderTrackingCommand(Guid.NewGuid(), OrderStatus.Finished);

        var action = async () =>
            await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(string.Format(OrderTrackingNotFoundException.error, command.OrderId.Value));
    }
}