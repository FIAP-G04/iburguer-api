using FIAP.Diner.Application.Cart;
using FIAP.Diner.Domain.Cart;

namespace FIAP.Diner.Tests.Application.Cart;

public class CartManagementHandlerTest
{
    private readonly ICartRepository _cartRepositoty;

    private readonly CartManagementHandler _manipulator;

    public CartManagementHandlerTest()
    {
        _cartRepositoty = Substitute.For<ICartRepository>();

        _manipulator = new CartManagementHandler(_cartRepositoty);
    }

    [Fact]
    public async Task ShouldAddItemToCart()
    {
        var command = new AddItemToCartCommand(Guid.NewGuid(), Guid.NewGuid(), 11.11M, 2);

        await _manipulator.Handle(command, default);

        await _cartRepositoty
            .Received()
            .Save(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                c.CustomerId2.Value == command.CustomerId), Arg.Any<CancellationToken>());

        await _cartRepositoty
            .Received()
            .Update(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                    c.CartItems.Any(ci =>
                        ci.ProductId.Value == command.ProductId &&
                        ci.Price == command.Price &&
                        ci.Quantity.Value == command.Quantity)),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldAddItemToCartWhenCartCreated()
    {
        var customerId = Guid.NewGuid();

        var cart = new Diner.Domain.Cart.Cart(customerId);

        var command = new AddItemToCartCommand(customerId, Guid.NewGuid(), 11.11M, 2);

        _cartRepositoty.Get(customerId, Arg.Any<CancellationToken>()).Returns(cart);

        await _manipulator.Handle(command, default);

        await _cartRepositoty
            .Received()
            .Update(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                    c.CartItems.Any(ci =>
                        ci.ProductId.Value == command.ProductId &&
                        ci.Price == command.Price &&
                        ci.Quantity.Value == command.Quantity)),
                Arg.Any<CancellationToken>());

        await _cartRepositoty.DidNotReceive()
            .Save(Arg.Any<Diner.Domain.Cart.Cart>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldRemoveItemFromCart()
    {
        var customerId = Guid.NewGuid();

        var cart = new Diner.Domain.Cart.Cart(customerId);

        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        cart.AddItem(productId, price, quantity);

        _cartRepositoty.Get(customerId, Arg.Any<CancellationToken>()).Returns(cart);

        var command = new RemoveItemFromCartCommand(customerId, productId);

        await _manipulator.Handle(command, default);

        await _cartRepositoty
            .Received()
            .Update(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                    c.Id == cart.Id &&
                    c.CartItems.Any(ci =>
                        ci.ProductId.Value == command.ProductId)),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldThrowErrorWhenRemovingItemOfInexistentCart()
    {
        var command = new RemoveItemFromCartCommand(Guid.NewGuid(), Guid.NewGuid());

        var action = async () => await _manipulator.Handle(command, default);

        await action
            .Should()
            .ThrowAsync<CartNotFoundException>()
            .WithMessage(string.Format(CartNotFoundException.error, command.CustomerId));
    }

    [Fact]
    public async Task ShouldGetCartFromCustomer()
    {
        var customerId = Guid.NewGuid();

        var cart = new Diner.Domain.Cart.Cart(customerId);

        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        cart.AddItem(productId, price, quantity);

        var cartItem = cart.CartItems.First(ci => ci.ProductId.Value == productId);

        var cartItemDetails = new List<CartItemDetails>
        {
            new()
            {
                Description = description,
                Price = price,
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = price * quantity
            }
        };

        var cartDetail = new CartDetails
        {
            CustomerId = customerId, CartItems = cartItemDetails, TotalPrice = cart.TotalPrice
        };

        _cartRepositoty.GetDetailed(customerId, Arg.Any<CancellationToken>()).Returns(cartDetail);

        var query = new GetCartItemsQuery(customerId);

        var result = await _manipulator.Handle(query, default);

        result.CustomerId.Should().Be(cart.CustomerId2.Value);
        result.TotalPrice.Should().Be(cart.TotalPrice);
        result.CartItems.Should().NotBeNull();
        result.CartItems.Should().NotBeEmpty();
        result.CartItems.Should().Contain(ci =>
            ci.ProductId == cartItem.ProductId.Value &&
            ci.Price == cartItem.Price &&
            ci.Quantity == cartItem.Quantity &&
            ci.TotalPrice == cartItem.TotalPrice);
    }

    [Fact]
    public async Task ShouldThrowErrorWhenGettingInexistentCart()
    {
        var query = new GetCartItemsQuery(Guid.NewGuid());

        var action = async () => await _manipulator.Handle(query, default);

        await action
            .Should()
            .ThrowAsync<CartNotFoundException>()
            .WithMessage(string.Format(CartNotFoundException.error, query.CustomerId));
    }

    [Fact]
    public async Task ShouldUpdateCartItemProductsInformation()
    {
        var carts = new List<Diner.Domain.Cart.Cart>();

        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;

        var cart1 = new Diner.Domain.Cart.Cart(Guid.NewGuid());
        cart1.AddItem(productId, price, 1);

        var cart2 = new Diner.Domain.Cart.Cart(Guid.NewGuid());
        cart2.AddItem(productId, price, 1);

        var cart3 = new Diner.Domain.Cart.Cart(Guid.NewGuid());
        cart3.AddItem(productId, price, 1);

        carts.Add(cart1);
        carts.Add(cart2);
        carts.Add(cart3);

        var newDescription = "def";
        var newPrice = 22.22M;

        var command = new UpdateCartItemProductInformation(productId, newPrice);

        _cartRepositoty
            .GetByProductInCart(Arg.Is<ProductId>(productId), Arg.Any<CancellationToken>())
            .Returns(carts);

        await _manipulator.Handle(command, default);

        await _cartRepositoty
            .Received(1)
            .Update(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                    c.Id == cart1.Id &&
                    c.CartItems.Any(ci => ci.ProductId.Value == productId &&
                                          ci.Price == newPrice)),
                Arg.Any<CancellationToken>());

        await _cartRepositoty
            .Received(1)
            .Update(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                    c.Id == cart2.Id &&
                    c.CartItems.Any(ci => ci.ProductId.Value == productId &&
                                          ci.Price == newPrice)),
                Arg.Any<CancellationToken>());

        await _cartRepositoty
            .Received(1)
            .Update(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                    c.Id == cart3.Id &&
                    c.CartItems.Any(ci => ci.ProductId.Value == productId &&
                                          ci.Price == newPrice)),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldCloseCart()
    {
        var customerId = Guid.NewGuid();

        var cart = new Diner.Domain.Cart.Cart(customerId);

        var productId = Guid.NewGuid();
        var description = "abc";
        var price = 11.11M;
        ushort quantity = 2;

        cart.AddItem(productId, price, quantity);

        _cartRepositoty.Get(customerId, Arg.Any<CancellationToken>()).Returns(cart);

        var command = new CloseCartCommand(customerId);

        await _manipulator.Handle(command, default);

        await _cartRepositoty
            .Received()
            .Update(Arg.Is<Diner.Domain.Cart.Cart>(c =>
                    c.Id == cart.Id &&
                    c.Closed == true),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenClosingInexistentCart()
    {
        var customerId = Guid.NewGuid();

        _cartRepositoty.Get(customerId, Arg.Any<CancellationToken>()).ReturnsNull();

        var command = new CloseCartCommand(customerId);

        var action = async () => await _manipulator.Handle(command, default);

        await action
            .Should()
            .ThrowAsync<CartNotFoundException>()
            .WithMessage(string.Format(CartNotFoundException.error, command.CustomerId));
    }
}