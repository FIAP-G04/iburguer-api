using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Tracking;

namespace FIAP.Diner.Application.OrderTracking.Tracking;

public class TrackingHandler : ICommandHandler<RegisterOrderTrackingCommand>,
                               ICommandHandler<UpdateOrderTrackingCommand>
{
    private readonly IOrderTrackingRepository _orderTrackingRepository;

    public TrackingHandler(IOrderTrackingRepository orderTrackingTrackingRepository)
    {
        _orderTrackingRepository = orderTrackingTrackingRepository;
    }

    public async Task Handle(RegisterOrderTrackingCommand command, CancellationToken cancellation)
    {
        var order = new Domain.Tracking.OrderTracking(command.OrderId, command.CustomerId);

        await _orderTrackingRepository.Save(order);
    }

    public async Task Handle(UpdateOrderTrackingCommand command, CancellationToken cancellation)
    {
        var order = await _orderTrackingRepository.GetByOrderId(command.OrderId);

        if (order == null)
            throw new OrderTrackingNotFoundException(command.OrderId);

        order.UpdateStatus(command.OrderStatus);

        await _orderTrackingRepository.Update(order);
    }
}