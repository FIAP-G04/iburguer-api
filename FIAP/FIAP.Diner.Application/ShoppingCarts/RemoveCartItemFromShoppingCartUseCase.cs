using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;
public interface  IRemoveCartItemFromShoppingCartUseCase
{
    Task RemoveCartItemFromShoppingCart(Guid shoppingCartId, Guid cartItemId, CancellationToken cancellation);
}

public class RemoveCartItemFromShoppingCartUseCase : IRemoveCartItemFromShoppingCartUseCase
{
    private readonly IShoppingCartRepository _repository;

    public RemoveCartItemFromShoppingCartUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));
        
        _repository = repository;
    }

    public async Task RemoveCartItemFromShoppingCart(Guid shoppingCartId, Guid cartItemId, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(shoppingCartId, cancellation);

        if (shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        shoppingCart.RemoveCartItem(cartItemId);

        await _repository.Update(shoppingCart, cancellation);
    }
}
