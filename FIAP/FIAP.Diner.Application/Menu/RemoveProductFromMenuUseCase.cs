using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public interface IRemoveProductFromMenuUseCase
{
    Task RemoveProductFromMenu(Guid productId, CancellationToken cancellation);
}

public class RemoveProductFromMenuUseCase : IRemoveProductFromMenuUseCase
{
    private readonly IProductRepository _repository;

    public RemoveProductFromMenuUseCase(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IProductRepository));

        _repository = repository;
    }

    public async Task RemoveProductFromMenu(Guid productId, CancellationToken cancellation)
    {
        var product = await _repository.GetById(productId, cancellation);

        if(product is null)
            throw new ProductNotFoundException(productId);

        await _repository.Remove(product, cancellation);
    }

}