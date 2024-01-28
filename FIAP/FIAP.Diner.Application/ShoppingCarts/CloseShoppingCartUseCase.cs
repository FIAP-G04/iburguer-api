using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;
public interface ICloseShoppingCartUseCase
{
    Task CloseShoppingCart(Guid shoppingCartId, CancellationToken cancellation);
}
    public class CloseShoppingCartUseCase : ICloseShoppingCartUseCase
{
    private readonly IShoppingCartRepository _repository;

    public CloseShoppingCartUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
    }
    public async Task CloseShoppingCart(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(shoppingCartId, cancellation);

        if (shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        shoppingCart.Close();

        await _repository.Update(shoppingCart, cancellation);
    }
}
