using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;
public interface IDecrementTheQuantityOfTheCartItemUseCase
{
    Task DecrementTheQuantityOfTheCartItem(DecrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation);
}

public class DecrementTheQuantityOfTheCartItemUseCase : IDecrementTheQuantityOfTheCartItemUseCase
{
    private readonly IShoppingCartRepository _repository;

    public DecrementTheQuantityOfTheCartItemUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
    }
    public async Task DecrementTheQuantityOfTheCartItem(DecrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(dto.ShoppingCartId, cancellation);

        if (shoppingCart is null)
            throw new ShoppingCartNotFoundException(dto.ShoppingCartId);

        shoppingCart.DecrementTheQuantityOfTheCartItem(dto.CartItemId, dto.quantity);

        await _repository.Update(shoppingCart, cancellation);
    }
}
