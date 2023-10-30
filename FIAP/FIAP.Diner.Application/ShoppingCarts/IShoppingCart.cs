namespace FIAP.Diner.Application.ShoppingCarts;

public interface IShoppingCart
{
    Task<Guid> CreateAnonymousShoppingCart(CancellationToken cancellation);

    Task<Guid> CreateCustomerShoppingCart(Guid customerId, CancellationToken cancellation);

    Task AddItemToShoppingCart(AddItemToShoppingCartDTO dto, CancellationToken cancellation);

    Task ClearShoppingCart(Guid shoppingCartId, CancellationToken cancellation);

    Task RemoveCartItemFromShoppingCart(Guid shoppingCartId, Guid cartItemId,
        CancellationToken cancellation);

    Task IncrementTheQuantityOfTheCartItem(IncrementTheQuantityOfTheCartItemDTO dto,
        CancellationToken cancellation);

    Task DecrementTheQuantityOfTheCartItem(DecrementTheQuantityOfTheCartItemDTO dto,
        CancellationToken cancellation);

    Task CloseShoppingCart(Guid shoppingCartId, CancellationToken cancellation);

    Task UpdateCartItemPriceThroughProduct(UpdateCartItemPriceThroughProductDTO dto,
        CancellationToken cancellation);
}