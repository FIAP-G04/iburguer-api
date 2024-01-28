using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.ShoppingCarts;

public interface ICreateCustomerShoppingCartUseCase
{
    Task<Guid> CreateCustomerShoppingCart(Guid customerId, CancellationToken cancellation);
}

public class CreateCustomerShoppingCartUseCase : ICreateCustomerShoppingCartUseCase
{
    private readonly IShoppingCartRepository _repository;

    public CreateCustomerShoppingCartUseCase(IShoppingCartRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
    }

    public async Task<Guid> CreateCustomerShoppingCart(Guid customerId, CancellationToken cancellation)
    {
        var shoppingCart = ShoppingCart.GenerateCustomerShoppingCart(customerId);

        await _repository.Save(shoppingCart, cancellation);

        return shoppingCart.Id;
    }
}
