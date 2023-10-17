namespace FIAP.Diner.Application.OrderTracking.Tracking;

public record RegisterOrderTrackingCommand(Guid OrderId, Guid CustomerId);