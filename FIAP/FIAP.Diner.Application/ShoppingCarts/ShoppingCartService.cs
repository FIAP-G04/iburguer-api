using FIAP.Diner.Application.Menu;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;

public class ShoppingCartService : IShoppingCart
{
    private readonly IShoppingCartRepository _repository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartService(IShoppingCartRepository repository, IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));
        ArgumentNullException.ThrowIfNull(productRepository, nameof(IProductRepository));

        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<Guid> CreateAnonymousShoppingCart(CancellationToken cancellation)
    {
        var shoppingCart = ShoppingCart.GenerateAnonymousShoppingCart();

        await _repository.Save(shoppingCart, cancellation);

        return shoppingCart.Id;
    }

    public async Task<Guid> CreateCustomerShoppingCart(Guid customerId,
        CancellationToken cancellation)
    {
        var shoppingCart = ShoppingCart.GenerateCustomerShoppingCart(customerId);

        await _repository.Save(shoppingCart, cancellation);

        return shoppingCart.Id;
    }

    public async Task AddItemToShoppingCart(AddItemToShoppingCartDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await Load(dto.ShoppingCartId, cancellation);
        var product = await _productRepository.GetById(dto.ProductId, cancellation);

        if (product is null)
            throw new ProductNotFoundException(dto.ProductId);

        shoppingCart.AddCartItem(dto.ProductId, product.Price, dto.Quantity);

        await _repository.Update(shoppingCart, cancellation);
    }

    public async Task ClearShoppingCart(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await Load(shoppingCartId, cancellation);

        shoppingCart.Clear();

        await _repository.Update(shoppingCart, cancellation);
    }

    public async Task RemoveCartItemFromShoppingCart(Guid shoppingCartId, Guid cartItemId, CancellationToken cancellation)
    {
        var shoppingCart = await Load(shoppingCartId, cancellation);

        shoppingCart.RemoveCartItem(cartItemId);

        await _repository.Update(shoppingCart, cancellation);
    }

    public async Task IncrementTheQuantityOfTheCartItem(IncrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await Load(dto.ShoppingCartId, cancellation);

        shoppingCart.IncrementTheQuantityOfTheCartItem(dto.CartItemId, dto.quantity);

        await _repository.Update(shoppingCart, cancellation);
    }

    public async Task DecrementTheQuantityOfTheCartItem(DecrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await Load(dto.ShoppingCartId, cancellation);

        shoppingCart.DecrementTheQuantityOfTheCartItem(dto.CartItemId, dto.quantity);

        await _repository.Update(shoppingCart, cancellation);
    }

    public async Task CloseShoppingCart(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await Load(shoppingCartId, cancellation);

        shoppingCart.Close();

        await _repository.Update(shoppingCart, cancellation);
    }

    public async Task UpdateCartItemPriceThroughProduct(UpdateCartItemPriceThroughProductDTO dto, CancellationToken cancellation)
    {
        var shoppingCart = await Load(dto.ShoppingCartId, cancellation);

        shoppingCart.UpdateItemPriceThroughProduct(dto.ProductId, dto.Price);

        await _repository.Update(shoppingCart, cancellation);
    }

    private async Task<ShoppingCart> Load(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await _repository.GetById(shoppingCartId, cancellation);

        if(shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        return shoppingCart;
    }
}