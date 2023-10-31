namespace FIAP.Diner.Application.ShoppingCarts;

public record DecrementTheQuantityOfTheCartItemDTO(Guid ShoppingCartId, Guid CartItemId, ushort quantity);