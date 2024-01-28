using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public interface IAddProductToMenuUseCase
{
    Task AddProductToMenu(ProductDTO dto, CancellationToken cancellation);
}

public class AddProductToMenuUseCase : IAddProductToMenuUseCase
{
    private readonly IProductRepository _repository;

    public AddProductToMenuUseCase(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IProductRepository));

        _repository = repository;
    }

    public async Task AddProductToMenu(ProductDTO dto, CancellationToken cancellation)
    {
        var product = new Product(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.Category,
            dto.PreparationTime,
            dto.Urls.Select(url => new Url(url)));

        await _repository.Save(product, cancellation);
    }
}