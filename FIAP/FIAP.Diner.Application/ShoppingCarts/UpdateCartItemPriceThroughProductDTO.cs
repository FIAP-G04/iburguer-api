namespace FIAP.Diner.Application.ShoppingCarts;

public record UpdateCartItemPriceThroughProductDTO(Guid ShoppingCartId, Guid ProductId, decimal Price);