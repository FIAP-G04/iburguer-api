using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu;

public interface IProductRepository : IRepository<Product>
{
    Task Save(Product product, CancellationToken cancellationToken);

    Task Update(Product product, CancellationToken cancellationToken);

    Task Remove(Product product, CancellationToken cancellationToken);

    Task<Product> GetById(ProductId id, CancellationToken cancellationToken);

    Task<IEnumerable<Product>> GetByCategory(Category category,
        CancellationToken cancellationToken);
}