using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task Save(Product product);

        Task Update(Product product);

        Task Remove(Product product);

        Task<Product> Get(ProductId id);
    }
}
