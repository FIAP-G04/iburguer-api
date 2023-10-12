using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Tracking;

public class OrderTrackingDomainService : IOrderTrackingDomainService
{
    private readonly IOrderTrackingRepository orderTrackingRepository;

    public OrderTrackingDomainService(IOrderTrackingRepository orderTrackingRepository)
    {
        this.orderTrackingRepository = orderTrackingRepository;
    }

    public async Task RegisterOrderTracking(Guid orderId, Guid customerId)
    {
        var order = new OrderTracking(orderId, customerId);

        await orderTrackingRepository.Save(order);
    }

    public async Task UpdateOrderTracking(Guid orderId, OrderStatus orderStatus)
    {
        var order = await orderTrackingRepository.GetByOrderId(orderId);

        if (order == null)
            throw new DomainException(OrderTrackingExceptions.OrderNotFound);

        order.UpdateStatus(orderStatus);

        await orderTrackingRepository.Update(order);
    }
}