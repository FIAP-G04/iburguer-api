using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Order;

public record OrderStatusUpdatedDomainEvent(OrderId OrderId, CustomerId3 CustomerId3,
        OrderTracking Status)
    : IDomainEvent;