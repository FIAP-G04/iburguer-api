using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Orders;

public record OrderStatusUpdatedDomainEvent(OrderId OrderId, CustomerId Customer,
        OrderTracking Status)
    : IDomainEvent;