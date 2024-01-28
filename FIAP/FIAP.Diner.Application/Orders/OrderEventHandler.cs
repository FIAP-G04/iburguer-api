using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Orders;

public class OrderEventHandler : IEventHandler<PaymentRequestedDomainEvent>,
                                 IEventHandler<PaymentConfirmedDomainEvent>
{
    private readonly IRegisterOrderUseCase _registerOrderUseCase;
    private readonly IConfirmOrderUseCase _confirmOrderUseCase;

    public OrderEventHandler(IRegisterOrderUseCase registerOrderUseCase, IConfirmOrderUseCase confirmOrderUseCase)
    {
        _registerOrderUseCase = registerOrderUseCase;
        _confirmOrderUseCase = confirmOrderUseCase;
    }

    public async Task Handle(PaymentRequestedDomainEvent evt, CancellationToken cancellation)
    {
        await _registerOrderUseCase.RegisterOrder(evt.ShoppingCartId, cancellation);
    }

    public async Task Handle(PaymentConfirmedDomainEvent evt, CancellationToken cancellation)
    {
        await _confirmOrderUseCase.ConfirmOrder(evt.ShoppingCartId, cancellation);
    }
}