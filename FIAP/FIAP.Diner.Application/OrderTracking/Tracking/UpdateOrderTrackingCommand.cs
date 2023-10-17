using FIAP.Diner.Domain.Tracking;

namespace FIAP.Diner.Application.OrderTracking.Tracking;

public record UpdateOrderTrackingCommand(Guid OrderId, OrderStatus OrderStatus);