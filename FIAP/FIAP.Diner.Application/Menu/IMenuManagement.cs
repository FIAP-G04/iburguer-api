using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public interface IMenuManagement
{
    Task AddProductToMenu(ProductDTO dto, CancellationToken cancellation);

    Task RemoveProductFromMenu(Guid productId, CancellationToken cancellation);

    Task ChangeMenuProduct(ProductDTO dto, CancellationToken cancellation);

    Task DisableMenuProduct(Guid productId, CancellationToken cancellation);

    Task EnableMenuProduct(Guid productId, CancellationToken cancellation);

    Task<IEnumerable<ProductDTO>> GetByCategory(Category category,
        CancellationToken cancellation);
}