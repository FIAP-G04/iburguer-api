using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;
public interface ICreateAnonymousShoppingCartUseCase
{
    Task<Guid> CreateAnonymousShoppingCart(CancellationToken cancellation);
}

public class CreateAnonymousShoppingCartUseCase : ICreateAnonymousShoppingCartUseCase
{
    private readonly IShoppingCartRepository _repository;

    public CreateAnonymousShoppingCartUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
    }

    public async Task<Guid> CreateAnonymousShoppingCart(CancellationToken cancellation)
    {
        var shoppingCart = ShoppingCart.GenerateAnonymousShoppingCart();

        await _repository.Save(shoppingCart, cancellation);

        return shoppingCart.Id;
    }
}
