using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Cart;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Application.Order.Tracking;

public class OrderRegisterEventHandler : IEventHandler<PaymentConfirmedDomainEvent>
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderRegisterEventHandler(IOrderRepository orderRepository,
        ICartRepository cartRepository)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
    }

    public async Task Handle(PaymentConfirmedDomainEvent @event, CancellationToken cancellation)
    {
        var customerId = await _cartRepository.GetCustomerId(@event.CartId, cancellation);

        var order = new Domain.Order.Order(@event.CartId, customerId.Value);

        var withdrawCode = await _orderRepository.GetNextWithdrawCode(cancellation);

        order.AddWithdrawCode(withdrawCode);

        await _orderRepository.Save(order, cancellation);
    }
}