using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu;

public class ProductImage :  Entity<string>
{
    public ProductId ProductId { get; private set; }

    public Url Url { get; private set; }

    private ProductImage() { }

    public ProductImage(ProductId productId, Url url)
    {
        ArgumentNullException.ThrowIfNull(productId, nameof(productId));

        Id = ProductImageId.New;
        ProductId = productId;
        Url = url;
    }
}