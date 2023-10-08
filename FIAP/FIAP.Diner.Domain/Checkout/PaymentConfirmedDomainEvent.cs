using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Checkout
{
    public record PaymentConfirmedDomainEvent(Guid OrderId) : IDomainEvent;
}
