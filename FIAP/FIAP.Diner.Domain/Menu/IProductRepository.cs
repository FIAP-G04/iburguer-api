using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu
{
    public interface IProductRepository : IRepository<Product>
    {
        Task Save(Product product, CancellationToken cancellationToken);

        Task Update(Product product, CancellationToken cancellationToken);

        Task Remove(Product product, CancellationToken cancellationToken);

        Task<Product> Get(ProductId id, CancellationToken cancellationToken);

        Task<IEnumerable<Product>> Get(ProductId? id, string? name, string? description, Category? category, CancellationToken cancellationToken);

        Task<IEnumerable<ProductDetails>> GetByCategory(Category category, CancellationToken cancellationToken);
    }
}
