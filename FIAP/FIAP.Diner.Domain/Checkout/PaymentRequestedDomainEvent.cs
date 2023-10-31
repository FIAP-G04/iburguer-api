using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Checkout;

public record PaymentRequestedDomainEvent(ShoppingCartId ShoppingCartId) : IDomainEvent;