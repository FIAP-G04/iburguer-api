using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Products
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _repository;

        public ProductDomainService(IProductRepository repository) => _repository = repository;

        public async Task Register(string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs)
        {
            var product = new Product(name, description, price, category, readyTimeExpectation, ImageURL.FromURLs(imageURLs));

            await _repository.Save(product);
        }

        public async Task Remove(ProductId id)
        {
            var product = await _repository.Get(id);

            if (product is null)
                throw new DomainException(ProductExceptions.ProductDoesNotExist);

            await _repository.Remove(product);
        }

        public async Task Update(ProductId id, string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs)
        {
            var product = await _repository.Get(id);

            if (product is null)
                throw new DomainException(ProductExceptions.ProductDoesNotExist);

            product.Update(name, description, price, category, readyTimeExpectation, ImageURL.FromURLs(imageURLs));

            await _repository.Update(product);
        }
    }
}
