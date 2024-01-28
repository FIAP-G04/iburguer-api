using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public interface IChangeMenuProductUseCase
{
    Task ChangeMenuProduct(ProductDTO dto, CancellationToken cancellation);

}

public class ChangeMenuProductUseCase : IChangeMenuProductUseCase
{
    private readonly IProductRepository _repository;

    public ChangeMenuProductUseCase(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IProductRepository));

        _repository = repository;
    }

    public async Task ChangeMenuProduct(ProductDTO dto, CancellationToken cancellation)
    {
        var product = await _repository.GetById(dto.ProductId, cancellation);

        if(product is null)
            throw new ProductNotFoundException(dto.ProductId);

        product.Update(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.Category,
            dto.PreparationTime,
            dto.Urls.Select(url => new Url(url)));

        await _repository.Update(product, cancellation);
    }
}