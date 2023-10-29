using FIAP.Diner.Application.Menu.Query;
using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Tests.Application.Menu.Query;

public class ProductsByCategoryQueryHandlerTest
{
    private readonly ProductsByCategoryQueryHandler _manipulator;
    private readonly IProductRepository _repository;

    public ProductsByCategoryQueryHandlerTest()
    {
        _repository = Substitute.For<IProductRepository>();

        _manipulator = new ProductsByCategoryQueryHandler(_repository);
    }

    [Fact]
    public async Task ShouldQueryProductsByCategory()
    {
        var product1 = new ProductDetails
        {
            ProductId = Guid.NewGuid(),
            Name = "abc1",
            Description = "description1",
            Price = 11.11M,
            Category = Category.MainDish,
            ImageURLs = new List<string> { "url1" }
        };
        var product2 = new ProductDetails
        {
            ProductId = Guid.NewGuid(),
            Name = "abc2",
            Description = "description2",
            Price = 11.12M,
            Category = Category.MainDish,
            ImageURLs = new List<string> { "url2" }
        };
        var product3 = new ProductDetails
        {
            ProductId = Guid.NewGuid(),
            Name = "abc3",
            Description = "description3",
            Price = 11.11M,
            Category = Category.MainDish,
            ImageURLs = new List<string> { "url3" }
        };

        var products = new List<ProductDetails> { product1, product2, product3 };

        var query = new GetProductsByCategoryQuery(Category.MainDish);

        _repository.GetByCategory(query.Category, Arg.Any<CancellationToken>()).Returns(products);

        var result = await _manipulator.Handle(query, default);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();

        result.Should().Contain(p => p.ProductId == product1.ProductId);
        var product1Result = result.Single(p => p.ProductId == product1.ProductId);
        product1Result.Name.Should().Be(product1.Name);
        product1Result.Description.Should().Be(product1.Description);
        product1Result.Price.Should().Be(product1.Price);
        product1Result.Category.Should().Be(product1.Category);
        product1.ImageURLs.Should().BeEquivalentTo(product1.ImageURLs);

        result.Should().Contain(p => p.ProductId == product2.ProductId);
        var product2Result = result.Single(p => p.ProductId == product2.ProductId);
        product2Result.Name.Should().Be(product2.Name);
        product2Result.Description.Should().Be(product2.Description);
        product2Result.Price.Should().Be(product2.Price);
        product2Result.Category.Should().Be(product2.Category);
        product2.ImageURLs.Should().BeEquivalentTo(product2.ImageURLs);

        result.Should().Contain(p => p.ProductId == product3.ProductId);
        var product3Result = result.Single(p => p.ProductId == product3.ProductId);
        product3Result.Name.Should().Be(product3.Name);
        product3Result.Description.Should().Be(product3.Description);
        product3Result.Price.Should().Be(product3.Price);
        product3Result.Category.Should().Be(product3.Category);
        product3.ImageURLs.Should().BeEquivalentTo(product3.ImageURLs);
    }
}