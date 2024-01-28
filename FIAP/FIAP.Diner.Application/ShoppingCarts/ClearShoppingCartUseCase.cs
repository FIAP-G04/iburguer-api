using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;
public interface IClearShoppingCartUseCase
{
    Task ClearShoppingCart(Guid shoppingCartId, CancellationToken cancellation);
}
public class ClearShoppingCartUseCase: IClearShoppingCartUseCase
{
    private readonly IShoppingCartRepository _repository;

    public ClearShoppingCartUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
    }

    public async Task ClearShoppingCart(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(shoppingCartId, cancellation);

        if (shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        shoppingCart.Clear();

        await _repository.Update(shoppingCart, cancellation);
    }
}
