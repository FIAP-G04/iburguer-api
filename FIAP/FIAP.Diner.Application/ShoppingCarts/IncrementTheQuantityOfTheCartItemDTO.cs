namespace FIAP.Diner.Application.ShoppingCarts;

public record IncrementTheQuantityOfTheCartItemDTO(Guid ShoppingCartId, Guid CartItemId, ushort quantity);