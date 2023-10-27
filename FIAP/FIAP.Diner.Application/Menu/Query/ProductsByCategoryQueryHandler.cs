using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu.Query
{
    public class ProductsByCategoryQueryHandler : IQueryHandler<GetProductsByCategoryQuery, IEnumerable<ProductDetails>>
    {
        private readonly IProductRepository _productRepository;

        public ProductsByCategoryQueryHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<IEnumerable<ProductDetails>> Handle(GetProductsByCategoryQuery query, CancellationToken cancellation)
            => await _productRepository.GetByCategory(query.Category, cancellation);
    }
}
