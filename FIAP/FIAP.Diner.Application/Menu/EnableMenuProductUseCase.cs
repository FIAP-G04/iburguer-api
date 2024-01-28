using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public interface IEnableMenuProductUseCase
{
    Task EnableMenuProduct(Guid productId, CancellationToken cancellation);
}

public class EnableMenuProductUseCase : IEnableMenuProductUseCase
{
    private readonly IProductRepository _repository;

    public EnableMenuProductUseCase(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IProductRepository));

        _repository = repository;
    }

    public async Task EnableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        var product = await _repository.GetById(productId, cancellation);

        if(product is null)
            throw new ProductNotFoundException(productId);

        product.Enable();

        await _repository.Update(product, cancellation);
    }
}