using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task Save(Product product);

        Task Update(Product product);

        Task Remove(Product product);

        Task<Product> Get(Guid id);
    }
}
