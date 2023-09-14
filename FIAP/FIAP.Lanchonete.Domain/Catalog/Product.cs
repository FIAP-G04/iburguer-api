namespace FIAP.Diner.Domain.Catalog;

public class Product
{
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
    Category Category { get; set; }
    IEnumerable<UrlImage> Images { get; set; }
}
