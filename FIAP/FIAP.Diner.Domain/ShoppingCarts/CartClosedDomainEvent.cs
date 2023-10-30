using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.ShoppingCarts;

public record CartClosedDomainEvent(ShoppingCartId ShoppingCartId) : IDomainEvent;