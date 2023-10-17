using FIAP.Diner.Domain.Cart;

namespace FIAP.Diner.Tests.Domain.Cart;

public class CartItemTest
{
    [Fact]
    public void ShouldCreateCartItem()
    {
        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        var cartItem = new CartItem(productId, price, quantity);

        cartItem.Id.Should().NotBeNull();
        cartItem.Id.Value.Should().NotBe(Guid.Empty);
        cartItem.ProductId.Value.Should().Be(productId);
        cartItem.Price.Should().Be(price);
    }

    [Fact]
    public void ShouldCalculateTotalPrice()
    {
        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        var cartItem = new CartItem(productId, price, quantity);

        cartItem.TotalPrice.Should().Be(quantity * price);
    }

    [Fact]
    public void ShouldIncreaseItem()
    {
        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        var cartItem = new CartItem(productId, price, quantity);

        cartItem.Increase();

        cartItem.Quantity.Value.Should().Be(quantity + 1);
    }

    [Fact]
    public void ShouldIncreaseItemSpecifyingQuantity()
    {
        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        var cartItem = new CartItem(productId, price, quantity);

        ushort quantityToIncrease = 5;

        cartItem.Increase(quantityToIncrease);

        cartItem.Quantity.Value.Should().Be(quantity + quantityToIncrease);
    }

    [Fact]
    public void ShouldDecreaseItem()
    {
        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        var cartItem = new CartItem(productId, price, quantity);

        cartItem.Decrease();

        cartItem.Quantity.Value.Should().Be(quantity - 1);
    }

    [Fact]
    public void ShouldUpdateCartItemInformation()
    {
        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        var cartItem = new CartItem(productId, price, quantity);

        var newDescription = "abc2";
        var newPrice = 22.22M;

        cartItem.Update(newPrice);

        cartItem.Price.Should().Be(newPrice);
    }
}