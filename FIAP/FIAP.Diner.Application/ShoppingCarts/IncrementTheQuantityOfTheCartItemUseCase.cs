using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;
public interface IIncrementTheQuantityOfTheCartItemUseCase
{
    Task IncrementTheQuantityOfTheCartItem(IncrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation);
}
public class IncrementTheQuantityOfTheCartItemUseCase : IIncrementTheQuantityOfTheCartItemUseCase
{
    private readonly IShoppingCartRepository _repository;

    public IncrementTheQuantityOfTheCartItemUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
    }

    public async Task IncrementTheQuantityOfTheCartItem(IncrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(dto.ShoppingCartId, cancellation);

        if (shoppingCart is null)
            throw new ShoppingCartNotFoundException(dto.ShoppingCartId);

        shoppingCart.IncrementTheQuantityOfTheCartItem(dto.CartItemId, dto.quantity);

        await _repository.Update(shoppingCart, cancellation);
    }
}
