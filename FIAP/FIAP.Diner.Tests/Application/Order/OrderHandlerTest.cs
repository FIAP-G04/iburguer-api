using FIAP.Diner.Application.Orders;
using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Tests.Application.Order;

public class OrderHandlerTest
{
    private readonly OrderService _manipulator;
    private readonly IOrderRepository orderRepository;

    public OrderHandlerTest()
    {
        orderRepository = Substitute.For<IOrderRepository>();

        _manipulator = new OrderService(orderRepository);
    }

    [Fact]
    public async Task ShouldUpdateOrderTrackingStatus()
    {
        var orderTracking = new Diner.Domain.Orders.Order(Guid.NewGuid(), Guid.NewGuid());

        var command = new UpdateOrderTrackingCommand(orderTracking.Id, OrderStatus.Ready);

        orderRepository.Get(orderTracking.Id, Arg.Any<CancellationToken>()).Returns(orderTracking);

        await _manipulator.Handle(command, default);

        await orderRepository.Received()
            .Update(Arg.Is<Diner.Domain.Orders.Order>(o =>
                o.Id == orderTracking.Id &&
                o.Status.OrderStatus == command.OrderStatus), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldThrowErrorWhenOrderDoesNotExist()
    {
        orderRepository.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

        var command = new UpdateOrderTrackingCommand(Guid.NewGuid(), OrderStatus.Finished);

        var action = async () =>
            await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(string.Format(OrderTrackingNotFoundException.error,
                command.OrderId.Value));
    }
}