using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders;

public class OrderEventHandler : IEventHandler<PaymentRequestedDomainEvent>,
                                 IEventHandler<PaymentConfirmedDomainEvent>
{
    private readonly IOrderService _orderService;

    public OrderEventHandler(IOrderService orderService)
    {
        ArgumentNullException.ThrowIfNull(orderService, nameof(IOrderService));

        _orderService = orderService;
    }

    public async Task Handle(PaymentRequestedDomainEvent evt, CancellationToken cancellation)
    {
        await _orderService.RegisterOrder(evt.ShoppingCartId, cancellation);
    }

    public async Task Handle(PaymentConfirmedDomainEvent evt, CancellationToken cancellation)
    {
        await _orderService.ConfirmOrder(evt.ShoppingCartId, cancellation);
    }
}