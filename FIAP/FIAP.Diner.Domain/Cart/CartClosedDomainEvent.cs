using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Cart;

public record CartClosedDomainEvent(Cart Cart) : IDomainEvent;