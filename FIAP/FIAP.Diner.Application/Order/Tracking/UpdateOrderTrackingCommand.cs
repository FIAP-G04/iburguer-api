using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Application.Order.Tracking;

public record UpdateOrderTrackingCommand(OrderId OrderId, OrderStatus OrderStatus);