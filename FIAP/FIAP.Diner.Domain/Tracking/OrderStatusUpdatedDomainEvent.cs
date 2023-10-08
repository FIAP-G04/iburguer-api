using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Tracking;

public record OrderStatusUpdatedDomainEvent(Guid OrderId, Guid CustomerId, TrackingStatus Status)
    : IDomainEvent;