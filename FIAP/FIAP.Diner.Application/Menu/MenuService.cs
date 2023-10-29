using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public class MenuService : IMenuManagement
{
    private readonly IProductRepository _repository;

    public MenuService(IProductRepository repository) => _repository = repository;

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

    public async Task RemoveProductFromMenu(Guid productId, CancellationToken cancellation)
    {
        var product = await Load(productId, cancellation);

        await _repository.Remove(product, cancellation);
    }

    public async Task ChangeMenuProduct(ProductDTO dto, CancellationToken cancellation)
    {
        var product = await Load(dto.ProductId, cancellation);

        product.Update(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.Category,
            dto.PreparationTime,
            dto.Urls.Select(url => new Url(url)));

        await _repository.Update(product, cancellation);
    }

    public async Task DisableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        var product = await Load(productId, cancellation);

        product.Disable();

        await _repository.Update(product, cancellation);
    }

    public async Task EnableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        var product = await Load(productId, cancellation);

        product.Enable();

        await _repository.Update(product, cancellation);
    }

    public async Task<IEnumerable<ProductDTO>> GetByCategory(Category category,
        CancellationToken cancellation)
    {
        var products = await _repository.GetByCategory(category, cancellation);

        return products.Select(product => ProductDTO.Map(product));
    }


    private async Task<Product> Load(Guid id, CancellationToken cancellation)
    {
        var product = await _repository.GetById(id, cancellation);

        if(product is null)
            throw new ProductNotFoundException(id);

        return product;
    }
}