using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.ShoppingCarts;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    Task Save(ShoppingCart shoppingCart, CancellationToken cancellationToken);

    Task Update(ShoppingCart shoppingCart, CancellationToken cancellationToken);

    Task<ShoppingCart?> GetById(ShoppingCartId id, CancellationToken cancellationToken);
}