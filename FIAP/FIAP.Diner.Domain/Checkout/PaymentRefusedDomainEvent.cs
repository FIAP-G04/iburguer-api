using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Checkout
{
    public record PaymentRefusedDomainEvent(Guid OrderId) : IDomainEvent;
}
