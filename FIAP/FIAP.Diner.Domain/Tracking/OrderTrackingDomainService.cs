using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Tracking;

public class OrderTrackingDomainService : IOrderTrackingDomainService
{
    private readonly IOrderRepository _orderRepository;

    public OrderTrackingDomainService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task RegisterOrderTracking(Guid orderId, Guid customerId)
    {
        var order = new OrderTracking(orderId, customerId);

        await _orderRepository.Save(order);
    }

    public async Task UpdateOrderTracking(Guid orderId, OrderStatus orderStatus)
    {
        var order = await _orderRepository.GetByOrderId(orderId);

        if (order == null)
            throw new DomainException(OrderTrackingExceptions.OrderNotFound);

        order.UpdateStatus(orderStatus);

        await _orderRepository.Update(order);
    }
}