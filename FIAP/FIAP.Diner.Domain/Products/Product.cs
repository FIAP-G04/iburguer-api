using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Products
{
    public class Product : Entity<Guid>, IAggregateRoot
    {public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Category Category { get; private set; }
        public TimeSpan ReadyTimeExpectation { get; private set; }

        private IEnumerable<ImageURL> _imageURLs;
        public IReadOnlyCollection<ImageURL> ImageURLs => _imageURLs.ToList().AsReadOnly();

        public Product(string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<ImageURL> imageURLs)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            ReadyTimeExpectation = readyTimeExpectation;
            _imageURLs = imageURLs;

            Validate();
        }

        public void Update(string name, string description, decimal price, Category category, TimeSpan readyTimeExpectation, IEnumerable<ImageURL> imageURLs)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            ReadyTimeExpectation = readyTimeExpectation;
            _imageURLs = imageURLs;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new DomainException(ProductExceptions.ProductNameIsRequired);

            if (string.IsNullOrEmpty(Description))
                throw new DomainException(ProductExceptions.ProductDescriptionIsRequired);

            if (!_imageURLs.Any())
                throw new DomainException(ProductExceptions.ProductImageURLIsRequired);
        }
    }
}
