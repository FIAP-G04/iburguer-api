using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Cart;

public interface ICartRepository : IRepository<Cart>
{
    Task Save(Cart cart, CancellationToken cancellationToken);

    Task Update(Cart cart, CancellationToken cancellationToken);

    Task<Cart> Get(CustomerId2 customerId2, CancellationToken cancellationToken);

    Task<CustomerId2> GetCustomerId(CartId cartId, CancellationToken cancellationToken);

    Task<CartDetails> GetDetailed(CustomerId2 customerId2, CancellationToken cancellationToken);

    Task<IEnumerable<Cart>> GetByProductInCart(ProductId productId,
        CancellationToken cancellationToken);
}