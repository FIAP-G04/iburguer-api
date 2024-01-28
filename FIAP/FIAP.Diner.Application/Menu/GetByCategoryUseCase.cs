using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public interface IGetByCategoryUseCase
{
    Task<IEnumerable<ProductDTO>> GetByCategory(Category category,
        CancellationToken cancellation);
}

public class GetByCategoryUseCase : IGetByCategoryUseCase
{
    private readonly IProductRepository _repository;

    public GetByCategoryUseCase(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IProductRepository));

        _repository = repository;
    }

    public async Task<IEnumerable<ProductDTO>> GetByCategory(Category category, CancellationToken cancellation)
    {
        var products = await _repository.GetByCategory(category, cancellation);

        return products.Select(product => ProductDTO.Map(product));
    }
}