using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Cart;
using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout.Requirement;

public class GeneratePaymentEventHandler : IEventHandler<CartClosedDomainEvent>
{
    private readonly IPaymentRepository _repository;

    public GeneratePaymentEventHandler(IPaymentRepository repository) => _repository = repository;

    public async Task Handle(CartClosedDomainEvent @event, CancellationToken cancellation)
    {
        var payment = new Payment(@event.Cart.Id, @event.Cart.TotalPrice);

        await _repository.Save(payment, cancellation);
    }
}