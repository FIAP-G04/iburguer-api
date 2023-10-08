using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.Products;
using FluentAssertions;

namespace FIAP.Diner.Tests.Domain.Products
{
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
            var imageUrls = new List<ImageURL>() { new("abc.url.com") };

            var product = new Product(name, description, price, category, readyTime, imageUrls);

            product.Name.Should().Be(name);
            product.Description.Should().Be(description);
            product.Price.Should().Be(price);
            product.Category.Should().Be(category);
            product.ReadyTimeExpectation.Should().Be(readyTime);
            product.ImageURLs.Should().BeEquivalentTo(imageUrls);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowErrorWhenInvalidName(string name)
        {
            var description = "Product description";
            var price = 11.11M;
            var category = Category.MainDish;
            var readyTime = TimeSpan.FromMinutes(30);
            var imageUrls = new List<ImageURL>() { new("abc.url.com") };

            var action = () => new Product(name, description, price, category, readyTime, imageUrls);

            action.Should().Throw<DomainException>()
                .WithMessage(ProductExceptions.ProductNameIsRequired);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowErrorWhenInvalidDescription(string description)
        {
            var name = "Product Name";
            var price = 11.11M;
            var category = Category.MainDish;
            var readyTime = TimeSpan.FromMinutes(30);
            var imageUrls = new List<ImageURL>() { new("abc.url.com") };

            var action = () => new Product(name, description, price, category, readyTime, imageUrls);

            action.Should().Throw<DomainException>()
                .WithMessage(ProductExceptions.ProductDescriptionIsRequired);
        }

        [Fact]
        public void ShouldThrowErrorWhenNoImageURLProvided()
        {
            var name = "Product Name";
            var description = "Product description";
            var price = 11.11M;
            var category = Category.MainDish;
            var readyTime = TimeSpan.FromMinutes(30);
            var imageUrls = new List<ImageURL>();

            var action = () => new Product(name, description, price, category, readyTime, imageUrls);

            action.Should().Throw<DomainException>()
                .WithMessage(ProductExceptions.ProductImageURLIsRequired);
        }

        [Fact]
        public void ShouldUpdateProduct()
        {
            var name = "Product Name";
            var description = "Product description";
            var price = 11.11M;
            var category = Category.MainDish;
            var readyTime = TimeSpan.FromMinutes(30);
            var imageUrls = new List<ImageURL>() { new("abc.url.com") };

            var product = new Product(name, description, price, category, readyTime, imageUrls);

            var nameUpdated = "Product Name Updated";
            var descriptionUpdated = "Product description Updated";
            var priceUpdated = 22.22M;
            var categoryUpdated = Category.Drink;
            var readyTimeUpdated = TimeSpan.FromMinutes(60);
            var imageUrlsUpdated = new List<ImageURL>() { new("def.url.com") };

            product.Update(nameUpdated, descriptionUpdated, priceUpdated, categoryUpdated, readyTimeUpdated, imageUrlsUpdated);

            product.Name.Should().Be(nameUpdated);
            product.Description.Should().Be(descriptionUpdated);
            product.Price.Should().Be(priceUpdated);
            product.Category.Should().Be(categoryUpdated);
            product.ReadyTimeExpectation.Should().Be(readyTimeUpdated);
            product.ImageURLs.Should().BeEquivalentTo(imageUrlsUpdated);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowErrorWhenUpdateProductWithInvalidName(string nameUpdated)
        {
            var name = "Product Name";
            var description = "Product description";
            var price = 11.11M;
            var category = Category.MainDish;
            var readyTime = TimeSpan.FromMinutes(30);
            var imageUrls = new List<ImageURL>() { new("abc.url.com") };

            var product = new Product(name, description, price, category, readyTime, imageUrls);
            
            var descriptionUpdated = "Product description Updated";
            var priceUpdated = 22.22M;
            var categoryUpdated = Category.Drink;
            var readyTimeUpdated = TimeSpan.FromMinutes(60);
            var imageUrlsUpdated = new List<ImageURL>() { new("def.url.com") };

            var action = () => product.Update(nameUpdated, descriptionUpdated, priceUpdated, categoryUpdated, readyTimeUpdated, imageUrlsUpdated);
            action.Should().Throw<DomainException>()
                .WithMessage(ProductExceptions.ProductNameIsRequired);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowErrorWhenUpdateProductWithInvalidDescription(string descriptionUpdated)
        {
            var name = "Product Name";
            var description = "Product description";
            var price = 11.11M;
            var category = Category.MainDish;
            var readyTime = TimeSpan.FromMinutes(30);
            var imageUrls = new List<ImageURL>() { new("abc.url.com") };

            var product = new Product(name, description, price, category, readyTime, imageUrls);

            var nameUpdated = "Product Name Updated";
            var priceUpdated = 22.22M;
            var categoryUpdated = Category.Drink;
            var readyTimeUpdated = TimeSpan.FromMinutes(60);
            var imageUrlsUpdated = new List<ImageURL>() { new("def.url.com") };

            var action = () => product.Update(nameUpdated, descriptionUpdated, priceUpdated, categoryUpdated, readyTimeUpdated, imageUrlsUpdated);
            action.Should().Throw<DomainException>()
                .WithMessage(ProductExceptions.ProductDescriptionIsRequired);
        }

        [Fact]
        public void ShouldThrowErrorWhenUpdateProductWithNoImageURL()
        {
            var name = "Product Name";
            var description = "Product description";
            var price = 11.11M;
            var category = Category.MainDish;
            var readyTime = TimeSpan.FromMinutes(30);
            var imageUrls = new List<ImageURL>() { new("abc.url.com") };

            var product = new Product(name, description, price, category, readyTime, imageUrls);

            var nameUpdated = "Product Name Updated";
            var descriptionUpdated = "Product description Updated";
            var priceUpdated = 22.22M;
            var categoryUpdated = Category.Drink;
            var readyTimeUpdated = TimeSpan.FromMinutes(60);
            var imageUrlsUpdated = new List<ImageURL>();

            var action = () => product.Update(nameUpdated, descriptionUpdated, priceUpdated, categoryUpdated, readyTimeUpdated, imageUrlsUpdated);
            action.Should().Throw<DomainException>()
                .WithMessage(ProductExceptions.ProductImageURLIsRequired);
        }
    }
}
