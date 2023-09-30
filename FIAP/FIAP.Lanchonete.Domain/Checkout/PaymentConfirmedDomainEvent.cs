using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Checkout
{
    public record PaymentConfirmedDomainEvent(Guid OrderId) : IDomainEvent;
}
