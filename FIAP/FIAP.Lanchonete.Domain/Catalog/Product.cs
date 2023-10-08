using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Catalog;

public class Product
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
    public IEnumerable<UrlImage> Images { get; set; }
}
