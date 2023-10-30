namespace FIAP.Diner.Application.ShoppingCarts;

public record AddItemToShoppingCartDTO(Guid ShoppingCartId, Guid ProductId, ushort Quantity);