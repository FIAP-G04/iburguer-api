using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;
public interface IUpdateCartItemPriceThroughProductUseCase
{
    Task UpdateCartItemPriceThroughProduct(UpdateCartItemPriceThroughProductDTO dto, CancellationToken cancellation);
}
public class UpdateCartItemPriceThroughProductUseCase : IUpdateCartItemPriceThroughProductUseCase
{
    private readonly IShoppingCartRepository _repository;


    public UpdateCartItemPriceThroughProductUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
    }
    public async Task UpdateCartItemPriceThroughProduct(UpdateCartItemPriceThroughProductDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(dto.ShoppingCartId, cancellation);

        if (shoppingCart is null)
            throw new ShoppingCartNotFoundException(dto.ShoppingCartId);

        shoppingCart.UpdateItemPriceThroughProduct(dto.ProductId, dto.Price);

        await _repository.Update(shoppingCart, cancellation);
    }
}
