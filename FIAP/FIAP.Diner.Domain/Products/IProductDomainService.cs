namespace FIAP.Diner.Domain.Products
{
    public interface IProductDomainService
    {
        Task Register(string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs);

        Task Update(ProductId id, string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs);

        Task Remove(ProductId id);
    }
}
