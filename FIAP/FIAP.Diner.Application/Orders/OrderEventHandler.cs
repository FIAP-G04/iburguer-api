using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Orders;

public class OrderEventHandler : IEventHandler<PaymentConfirmedDomainEvent>
{
    private readonly IConfirmOrderUseCase _confirmOrderUseCase;

    public OrderEventHandler(IConfirmOrderUseCase confirmOrderUseCase)
    {
        _confirmOrderUseCase = confirmOrderUseCase;
    }

    public async Task Handle(PaymentConfirmedDomainEvent evt, CancellationToken cancellation)
    {
        await _confirmOrderUseCase.ConfirmOrder(evt.ShoppingCartId, cancellation);
    }
}