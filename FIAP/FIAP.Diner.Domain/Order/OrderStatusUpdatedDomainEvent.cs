using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Order;

public record OrderStatusUpdatedDomainEvent(OrderId OrderId, CustomerId CustomerId, OrderTracking Status)
    : IDomainEvent;