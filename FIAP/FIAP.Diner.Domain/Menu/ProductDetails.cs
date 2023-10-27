namespace FIAP.Diner.Domain.Menu
{
    public class ProductDetails
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public IEnumerable<string> ImageURLs { get; set; }
    }
}
