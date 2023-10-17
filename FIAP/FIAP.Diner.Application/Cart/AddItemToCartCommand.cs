namespace FIAP.Diner.Application.Cart;

public record AddItemToCartCommand(Guid CustomerId, Guid ProductId, decimal Price, ushort Quantity);