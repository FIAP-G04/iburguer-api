using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Cart;

public interface ICartRepository : IRepository<Cart>
{
    Task Save(Cart cart, CancellationToken cancellationToken);

    Task Update(Cart cart, CancellationToken cancellationToken);

    Task<Cart> Get(CustomerId customerId, CancellationToken cancellationToken);

    Task<CartDetails> GetDetailed(CustomerId customerId, CancellationToken cancellationToken);

    Task<IEnumerable<Cart>> GetByProductInCart(ProductId productId, CancellationToken cancellationToken);
}