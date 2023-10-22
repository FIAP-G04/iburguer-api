namespace FIAP.Diner.Application.Order.Tracking;

public record RegisterOrderCommand(Guid CartId, Guid CustomerId);