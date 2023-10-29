using FIAP.Diner.Application.Menu;
using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Tests.Application.Menu.Management;

public class ProductManagementHandlerTest
{
    private readonly IMenuManagement _manipulator;

    private readonly Product _product;
    private readonly IProductRepository _repository;

    public ProductManagementHandlerTest()
    {
        _repository = Substitute.For<IProductRepository>();

        _manipulator = new MenuService(_repository);

        _product = new Product(
            "Product Name", "ProductDescription", 11.11M,
            Category.Dessert, TimeSpan.FromMinutes(10),
            new List<string> { "abc.com" });
    }

    [Fact]
    public async Task ShouldRegisterProduct()
    {
        var dto = new ProductDTO(
            _product.Name,
            _product.Description,
            _product.Price,
            _product.Category,
            _product.PreparationTime,
            _product.Urls.Select(u => u.)));

        await _manipulator.AddProductToMenu(dto, default);

        await _repository
            .Received()
            .Save(Arg.Is<Product>(p =>
                    p.Name == _product.Name &&
                    p.Description == _product.Description &&
                    p.Price == _product.Price &&
                    p.Category == _product.Category &&
                    p.ReadyTimeExpectation == _product.ReadyTimeExpectation &&
                    p.ImageURLs.Single(u => u.Url == _product.ImageURLs.First().Url) != null),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldUpdateProduct()
    {
        var nameUpdated = "Product Name Updated";
        var descriptionUpdated = "Product description Updated";
        var priceUpdated = 22.22M;
        var categoryUpdated = Category.Drink;
        var readyTimeUpdated = TimeSpan.FromMinutes(60);
        var imageUrlsUpdated = new List<Url> { new("def.url.com") };

        _repository.Get(_product.Id, Arg.Any<CancellationToken>()).Returns(_product);

        var command = new UpdateProductCommand(
            _product.Id.Value, nameUpdated, descriptionUpdated, priceUpdated,
            categoryUpdated, readyTimeUpdated, imageUrlsUpdated.Select(i => i.Url));

        await _manipulator.Handle(command, default);

        await _repository
            .Received()
            .Update(Arg.Is<Product>(p =>
                    p.Id == _product.Id &&
                    p.Name == nameUpdated &&
                    p.Description == descriptionUpdated &&
                    p.Price == priceUpdated &&
                    p.Category == categoryUpdated &&
                    p.ReadyTimeExpectation == readyTimeUpdated &&
                    p.ImageURLs.Single(u => u.Url == _product.ImageURLs.First().Url) != null),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldThrowErrorWhenUpdatingUnexistingProduct()
    {
        var nameUpdated = "Product Name Updated";
        var descriptionUpdated = "Product description Updated";
        var priceUpdated = 22.22M;
        var categoryUpdated = Category.Drink;
        var readyTimeUpdated = TimeSpan.FromMinutes(60);
        var imageUrlsUpdated = new List<Url> { new("def.url.com") };

        _repository.Get(_product.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        var command = new UpdateProductCommand(
            _product.Id.Value, nameUpdated, descriptionUpdated, priceUpdated,
            categoryUpdated, readyTimeUpdated, imageUrlsUpdated.Select(i => i.Url));

        var action = async () => await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<ProductNotFoundException>()
            .WithMessage(string.Format(ProductNotFoundException.error, command.ProductId));
    }

    [Fact]
    public async Task ShouldRemoveProduct()
    {
        _repository.Get(_product.Id, Arg.Any<CancellationToken>()).Returns(_product);

        var command = new RemoveProductToMenuCommand(_product.Id.Value);

        await _manipulator.Handle(command, default);

        await _repository
            .Received()
            .Remove(Arg.Is<Product>(p => p.Id == _product.Id),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldThrowErrorWhenRemovingUnexistingProduct()
    {
        _repository.Get(_product.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        var command = new RemoveProductToMenuCommand(_product.Id.Value);

        var action = async () => await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<ProductNotFoundException>()
            .WithMessage(string.Format(ProductNotFoundException.error, command.ProductId));
    }
}