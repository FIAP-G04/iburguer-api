using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Tests.Domain.Menu;

public class ProductTest
{
    [Fact]
    public void ShouldCreateProduct()
    {
        var name = "Product Name";
        var description = "Product description";
        var price = 11.11M;
        var category = Category.MainDish;
        var readyTime = TimeSpan.FromMinutes(30);
        var imageUrls = new List<string> { "abc.url.com" };

        var product = new Product(name, description, price, category, readyTime, imageUrls);

        product.Id.Should().NotBeNull();
        product.Id.Value.Should().NotBe(Guid.Empty);
        product.Name.Should().Be(name);
        product.Description.Should().Be(description);
        product.Price.Should().Be(price);
        product.Category.Should().Be(category);
        product.ReadyTimeExpectation.Should().Be(readyTime);
        product.ImageURLs.Should().ContainSingle(u => u.Url.Equals("abc.url.com"));
    }

    [Fact]
    public void ShouldUpdateProduct()
    {
        var name = "Product Name";
        var description = "Product description";
        var price = 11.11M;
        var category = Category.MainDish;
        var readyTime = TimeSpan.FromMinutes(30);
        var imageUrls = new List<string> { "abc.url.com" };

        var product = new Product(name, description, price, category, readyTime, imageUrls);

        var nameUpdated = "Product Name Updated";
        var descriptionUpdated = "Product description Updated";
        var priceUpdated = 22.22M;
        var categoryUpdated = Category.Drink;
        var readyTimeUpdated = TimeSpan.FromMinutes(60);
        var imageUrlsUpdated = new List<string> { "def.url.com" };

        product.Update(nameUpdated, descriptionUpdated, priceUpdated, categoryUpdated,
            readyTimeUpdated, imageUrlsUpdated);

        product.Name.Should().Be(nameUpdated);
        product.Description.Should().Be(descriptionUpdated);
        product.Price.Should().Be(priceUpdated);
        product.Category.Should().Be(categoryUpdated);
        product.ReadyTimeExpectation.Should().Be(readyTimeUpdated);
        product.ImageURLs.Should().ContainSingle(u => u.Url.Equals("def.url.com"));

        product.Events.Should().HaveCount(1);
        var raisedEvent =
            product.Events.First(e => e.GetType().Equals(typeof(ProductUpdatedDomainEvent))) as
                ProductUpdatedDomainEvent;

        raisedEvent.Should().NotBeNull();
        raisedEvent?.ProductId.Should().Be(product.Id);
        raisedEvent?.Price.Should().Be(product.Price);
    }

    [Fact]
    public void ShouldNotRaiseEventWhenPriceIsTheSame()
    {
        var name = "Product Name";
        var description = "Product description";
        var price = 11.11M;
        var category = Category.MainDish;
        var readyTime = TimeSpan.FromMinutes(30);
        var imageUrls = new List<string> { "abc.url.com" };

        var product = new Product(name, description, price, category, readyTime, imageUrls);

        var nameUpdated = "Product Name Updated";
        var descriptionUpdated = "Product description Updated";
        var priceUpdated = price;
        var categoryUpdated = Category.Drink;
        var readyTimeUpdated = TimeSpan.FromMinutes(60);
        var imageUrlsUpdated = new List<string> { "def.url.com" };

        product.Update(nameUpdated, descriptionUpdated, priceUpdated, categoryUpdated,
            readyTimeUpdated, imageUrlsUpdated);

        product.Price.Should().Be(price);
        product.Events.Should().BeEmpty();
    }
}