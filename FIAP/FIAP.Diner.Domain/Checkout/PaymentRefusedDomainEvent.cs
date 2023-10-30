using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Domain.Checkout;

public record PaymentRefusedDomainEvent(ShoppingCartId ShoppingCartId) : IDomainEvent;