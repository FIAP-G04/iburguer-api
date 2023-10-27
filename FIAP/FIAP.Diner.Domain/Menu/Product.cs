using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu
{
    public class Product : Entity<ProductId>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Category Category { get; private set; }
        public TimeSpan ReadyTimeExpectation { get; private set; }

        private IEnumerable<ImageURL> _imageURLs;
        public IReadOnlyCollection<ImageURL> ImageURLs => _imageURLs.ToList().AsReadOnly();

        public Product(string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            ReadyTimeExpectation = readyTimeExpectation;
            _imageURLs = ImageURL.FromURLs(imageURLs);
        }

        public void Update(string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<string> imageURLs)
        {
            if (price != Price)
                RaiseEvent(new ProductUpdatedDomainEvent(Id, price));

            Name = name;
            Description = description;
            Price = price;
            Category = category;
            ReadyTimeExpectation = readyTimeExpectation;
            _imageURLs = ImageURL.FromURLs(imageURLs);
        }
    }
}
