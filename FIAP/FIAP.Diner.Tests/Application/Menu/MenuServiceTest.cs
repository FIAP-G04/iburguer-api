using FIAP.Diner.Application.Menu;
using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Tests.Application.Menu
{
    public class MenuServiceTest
    {
        private readonly IProductRepository _productRepository;

        private readonly MenuService _manipulator;

        public MenuServiceTest()
        {
            _productRepository = Substitute.For<IProductRepository>();

            _manipulator = new(_productRepository);
        }

        [Fact]
        public async Task ShouldAddProductToMenu()
        {
            var dto = new ProductDTO(
                Guid.NewGuid(),
                "productName",
                "productDesc",
                11.11M,
                Category.MainDish,
                10,
                new string[] { "http://abc.com" });

            await _manipulator.AddProductToMenu(dto, default);

            await _productRepository.Received().Save(Arg.Is<Product>(p =>
                p.Name == dto.Name &&
                p.Description == dto.Description &&
                p.PreparationTime == dto.PreparationTime &&
                p.Price == dto.Price &&
                !p.Urls.Zip(dto.Urls).Any(z => z.First != z.Second)),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldRemoveProductFromMenu()
        {
            var product = new Product("productName",
                "productDesc",
                11.11M,
                Category.MainDish,
                10,
                new List<Url>() { new("http://abc.com") });

            _productRepository.GetById(product.Id, Arg.Any<CancellationToken>()).Returns(product);

            await _manipulator.RemoveProductFromMenu(product.Id, default);

            await _productRepository.Received()
                .Remove(Arg.Is<Product>(p => p.Id == product.Id),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldThrowErrorWhenRemovingUnexistentProduct()
        {
            var productId = Guid.NewGuid();

            _productRepository.GetById(productId, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _manipulator.RemoveProductFromMenu(productId, default);

            await action.Should().ThrowAsync<ProductNotFoundException>()
                .WithMessage(string.Format(ProductNotFoundException.error, productId));
        }

        [Fact]
        public async Task ShouldChangeMenuProduct()
        {
            var product = new Product("productName",
                "productDesc",
                11.11M,
                Category.MainDish,
                10,
                new List<Url>() { new("http://abc.com") });

            var dto = new ProductDTO(
                product.Id,
                "productName2",
                "productDesc2",
                11.12M,
                Category.MainDish,
                11,
                new string[] { "http://def.com" });

            _productRepository.GetById(product.Id, Arg.Any<CancellationToken>()).Returns(product);

            await _manipulator.ChangeMenuProduct(dto, default);

            await _productRepository.Received()
                .Update(Arg.Is<Product>(p =>
                    p.Id == dto.ProductId &&
                    p.Name == dto.Name &&
                    p.Description == dto.Description &&
                    p.Category == dto.Category &&
                    p.Description == dto.Description &&
                    p.Category == dto.Category &&
                    !p.Urls.Zip(dto.Urls).Any(z => z.First != z.Second)),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldThrowErrorWhenChaningUnexistentProduct()
        {
            var productId = Guid.NewGuid();

            var dto = new ProductDTO(
                productId,
                "productName2",
                "productDesc2",
                11.12M,
                Category.MainDish,
                11,
                new string[] { "http://def.com" });

            _productRepository.GetById(productId, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _manipulator.ChangeMenuProduct(dto, default);

            await action.Should().ThrowAsync<ProductNotFoundException>()
                .WithMessage(string.Format(ProductNotFoundException.error, productId));
        }

        [Fact]
        public async Task ShouldDisableMenuProduct()
        {
            var product = new Product("productName",
                "productDesc",
                11.11M,
                Category.MainDish,
                10,
                new List<Url>() { new("http://abc.com") });

            _productRepository.GetById(product.Id, Arg.Any<CancellationToken>()).Returns(product);

            await _manipulator.DisableMenuProduct(product.Id, default);

            await _productRepository.Received()
                .Update(Arg.Is<Product>(p => p.Id == product.Id && p.Enabled == false),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldEnableMenuProduct()
        {
            var product = new Product("productName",
                "productDesc",
                11.11M,
                Category.MainDish,
                10,
                new List<Url>() { new("http://abc.com") });
            product.Disable();

            _productRepository.GetById(product.Id, Arg.Any<CancellationToken>()).Returns(product);

            await _manipulator.EnableMenuProduct(product.Id, default);

            await _productRepository.Received()
                .Update(Arg.Is<Product>(p => p.Id == product.Id && p.Enabled == true),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldGetProductsByCategory()
        {
            var product1 = new Product("productName1",
                "productDesc1",
                11.11M,
                Category.MainDish,
                10,
                new List<Url>() { new("http://abc.com") });

            var product2 = new Product("productName2",
                "productDesc2",
                22.22M,
                Category.MainDish,
                20,
                new List<Url>() { new("http://def.com") });

            var products = new List<Product>() { product1, product2 };

            _productRepository.GetByCategory(Category.MainDish, Arg.Any<CancellationToken>()).Returns(products);

            var result = await _manipulator.GetByCategory(Category.MainDish, default);

            result.Should().NotBeEmpty();
            result.Count().Should().Be(2);
        }
    }
}
