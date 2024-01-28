using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public interface IDisableMenuProductUseCase
{
    Task DisableMenuProduct(Guid productId, CancellationToken cancellation);
}

public class DisableMenuProductUseCase : IDisableMenuProductUseCase
{
    private readonly IProductRepository _repository;

    public DisableMenuProductUseCase(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IProductRepository));

        _repository = repository;
    }

    public async Task DisableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        var product = await _repository.GetById(productId, cancellation);

        if(product is null)
            throw new ProductNotFoundException(productId);

        product.Disable();

        await _repository.Update(product, cancellation);
    }
}