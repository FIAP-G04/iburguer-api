using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu;

public record ProductDTO(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    Category Category,
    ushort PreparationTime,
    string[] Urls)
{
    public static ProductDTO Map(Product product)
    {
        return new ProductDTO(product.Id, product.Name, product.Description, product.Price,
            product.Category, product.PreparationTime, product.Urls.ToArray());
    }
}