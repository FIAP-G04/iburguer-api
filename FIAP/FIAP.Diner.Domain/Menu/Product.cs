using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Menu;

public class Product : Entity<ProductId>, IAggregateRoot
{
    private IList<ProductImage> _images = new List<ProductImage>();
    private Price _price;

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Price Price
    {
        get => _price;
        private set
        {
            if (_price != value)
            {
                RaiseEvent(new ProductUpdatedDomainEvent(Id, value, _price));
            }

            _price = value;
        }
    }

    public Category Category { get; private set; }

    public PreparationTime PreparationTime { get; private set; }

    public bool Enabled { get; private set; } = true;

    public IReadOnlyCollection<ProductImage> Images => _images.ToList().AsReadOnly();

    public IReadOnlyCollection<string> Urls => _images.Select(i => i.Url.ToString()).ToList();

    private Product() {}

    public Product(string name, string description, Price price, Category category,
        ushort preparationTime, IEnumerable<Url> images)
    {
        Id = ProductId.New;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        PreparationTime = preparationTime;

        SetImages(images);
    }

    public void Update(string name, string description, Price price, Category category,
        ushort preparationTime, IEnumerable<Url> images)
    {
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        PreparationTime = preparationTime;

        SetImages(images);
    }

    public void Enable() => Enabled = true;

    public void Disable() => Enabled = false;

    private void SetImages(IEnumerable<Url> urls)
    {
        if (_images.Any())
        {
            _images.Clear();
        }

        foreach (var url in urls)
        {
            _images.Add(new ProductImage(Id, url));
        }
    }
}