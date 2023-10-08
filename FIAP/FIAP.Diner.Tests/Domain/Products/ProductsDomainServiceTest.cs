using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.Products;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FIAP.Diner.Tests.Domain.Products
{
    public class ProductsDomainServiceTest
    {
        private readonly IProductRepository _repository;

        private readonly ProductDomainService _manipulator;

        private readonly Product _product;

        public ProductsDomainServiceTest()
        {
            _repository = Substitute.For<IProductRepository>();

            _manipulator = new(_repository);

            _product = new Product(
                "Product Name", "ProductDescription", 11.11M,
                Category.Dessert, TimeSpan.FromMinutes(10),
                new List<ImageURL>() { new("abc.com") });
        }

        [Fact]
        public async Task ShouldRegisterProduct()
        {
            await _manipulator.Register(
                _product.Name, _product.Description, _product.Price,
                _product.Category, _product.ReadyTimeExpectation, _product.ImageURLs.Select(x => x.Url));

            await _repository
                .Received()
                .Save(Arg.Is<Product>(p =>
                    p.Name == _product.Name &&
                    p.Description == _product.Description &&
                    p.Price == _product.Price &&
                    p.Category == _product.Category &&
                    p.ReadyTimeExpectation == _product.ReadyTimeExpectation &&
                    p.ImageURLs.SequenceEqual(_product.ImageURLs)));
        }

        [Fact]
        public async Task ShouldUpdateProduct()
        {
            var nameUpdated = "Product Name Updated";
            var descriptionUpdated = "Product description Updated";
            var priceUpdated = 22.22M;
            var categoryUpdated = Category.Drink;
            var readyTimeUpdated = TimeSpan.FromMinutes(60);
            var imageUrlsUpdated = new List<ImageURL>() { new("def.url.com") };

            _repository.Get(_product.Id).Returns(_product);

            await _manipulator.Update(
                _product.Id, nameUpdated, descriptionUpdated, priceUpdated,
                categoryUpdated, readyTimeUpdated, imageUrlsUpdated.Select(i => i.Url));

            await _repository
                .Received()
                .Update(Arg.Is<Product>(p =>
                    p.Id == _product.Id &&
                    p.Name == nameUpdated &&
                    p.Description == descriptionUpdated &&
                    p.Price == priceUpdated &&
                    p.Category == categoryUpdated &&
                    p.ReadyTimeExpectation == readyTimeUpdated &&
                    p.ImageURLs.SequenceEqual(imageUrlsUpdated)));
        }

        [Fact]
        public async Task ShouldThrowErrorWhenUpdatingUnexistingProduct()
        {
            var nameUpdated = "Product Name Updated";
            var descriptionUpdated = "Product description Updated";
            var priceUpdated = 22.22M;
            var categoryUpdated = Category.Drink;
            var readyTimeUpdated = TimeSpan.FromMinutes(60);
            var imageUrlsUpdated = new List<ImageURL>() { new("def.url.com") };

            _repository.Get(_product.Id).ReturnsNull();

            var action = async () => await _manipulator.Update(
                _product.Id, nameUpdated, descriptionUpdated, priceUpdated,
                categoryUpdated, readyTimeUpdated, imageUrlsUpdated.Select(i => i.Url));

            await action.Should().ThrowAsync<DomainException>()
                .WithMessage(ProductExceptions.ProductDoesNotExist);
        }

        [Fact]
        public async Task ShouldRemoveProduct()
        {
            _repository.Get(_product.Id).Returns(_product);

            await _manipulator.Remove(_product.Id);

            await _repository
                .Received()
                .Remove(Arg.Is<Product>(p => p.Id == _product.Id));
        }

        [Fact]
        public async Task ShouldThrowErrorWhenRemovingUnexistingProduct()
        {
            _repository.Get(_product.Id).ReturnsNull();

            var action = async () => await _manipulator.Remove(_product.Id);

            await action.Should().ThrowAsync<DomainException>()
                .WithMessage(ProductExceptions.ProductDoesNotExist);
        }
    }
}
