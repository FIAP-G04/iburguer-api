using FIAP.Diner.Application.Menu;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;

public interface IAddItemToShoppingCartUseCase
{
    Task AddItemToShoppingCart(AddItemToShoppingCartDTO dto, CancellationToken cancellation);
}

public class AddItemToShoppingCartUseCase : IAddItemToShoppingCartUseCase
{
    private readonly IShoppingCartRepository _repository;
    private readonly IProductRepository _productRepository;

    public AddItemToShoppingCartUseCase(IShoppingCartRepository repository, IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));
        ArgumentNullException.ThrowIfNull(productRepository, nameof(IProductRepository));

        _repository = repository;
        _productRepository = productRepository;
    }
    public async Task AddItemToShoppingCart(AddItemToShoppingCartDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(dto.ShoppingCartId, cancellation);

        if (shoppingCart is null)
            throw new ShoppingCartNotFoundException(dto.ShoppingCartId);

        var product = await _productRepository.GetById(dto.ProductId, cancellation);

        if (product is null)
            throw new ProductNotFoundException(dto.ProductId);

        shoppingCart.AddCartItem(dto.ProductId, product.Price, dto.Quantity);

        await _repository.Update(shoppingCart, cancellation);
    }

}
