namespace FIAP.Diner.Domain.Products
{
    public interface IProductDomainService
    {
        Task Register(string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs);

        Task Update(Guid id, string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs);

        Task Remove(Guid id);
    }
}
