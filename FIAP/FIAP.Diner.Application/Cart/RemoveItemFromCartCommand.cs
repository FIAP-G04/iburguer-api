namespace FIAP.Diner.Application.Cart;

public record RemoveItemFromCartCommand(Guid CustomerId, Guid ProductId, bool RemoveAll = false);