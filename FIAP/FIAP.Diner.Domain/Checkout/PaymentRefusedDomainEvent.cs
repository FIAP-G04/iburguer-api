using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Checkout
{
    public record PaymentRefusedDomainEvent(Guid OrderId) : IDomainEvent;
}
