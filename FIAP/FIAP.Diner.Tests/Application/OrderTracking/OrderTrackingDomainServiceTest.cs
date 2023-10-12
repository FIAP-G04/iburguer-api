using FIAP.Diner.Application.OrderTracking.Tracking;
using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Tracking;

namespace FIAP.Diner.Tests.Application.OrderTracking;

public class OrderTrackingDomainServiceTest
{
    private readonly IOrderTrackingRepository orderTrackingRepository;

    private readonly TrackingHandler _manipulator;

    public OrderTrackingDomainServiceTest()
    {
        orderTrackingRepository = Substitute.For<IOrderTrackingRepository>();

        _manipulator = new(orderTrackingRepository);
    }

    [Fact]
    public async Task ShouldRegisterOrderTracking()
    {
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var command = new RegisterOrderTrackingCommand(orderId, customerId);

        await _manipulator.Handle(command, default);

        await orderTrackingRepository.Received()
            .Save(Arg.Is<Diner.Domain.Tracking.OrderTracking>(o =>
                o.OrderId == orderId &&
                o.CustomerId == customerId));
    }

    [Fact]
    public async Task ShouldUpdateOrderTrackingStatus()
    {
        var orderTracking = new Diner.Domain.Tracking.OrderTracking(Guid.NewGuid(), Guid.NewGuid());

        var command = new UpdateOrderTrackingCommand(orderTracking.OrderId, OrderStatus.Ready);

        orderTrackingRepository.GetByOrderId(orderTracking.OrderId).Returns(orderTracking);

        await _manipulator.Handle(command, default);

        await orderTrackingRepository.Received()
            .Update(Arg.Is<Diner.Domain.Tracking.OrderTracking>(o =>
                o.Id == orderTracking.Id &&
                o.Status.OrderStatus == command.OrderStatus));
    }

    [Fact]
    public async Task ShouldThrowErrorWhenOrderDoesNotExist()
    {
        orderTrackingRepository.GetByOrderId(Arg.Any<Guid>()).ReturnsNull();

        var command = new UpdateOrderTrackingCommand(Guid.NewGuid(), OrderStatus.Finished);

        var action = async () =>
            await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(string.Format(OrderTrackingNotFoundException.error, command.OrderId));
    }
}