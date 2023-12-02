// using FIAP.Diner.Domain.Abstractions;
// using FIAP.Diner.Domain.ShoppingCarts;
//
// namespace FIAP.Diner.Tests.Domain.Cart;
//
// public class CartTest
// {
//     [Fact]
//     public void ShouldCreateCart()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         cart.Id.Should().NotBeNull();
//         cart.Id.Value.Should().NotBe(Guid.Empty);
//         cart.CustomerId2.Value.Should().Be(customerId);
//         cart.CartItems.Should().NotBeNull();
//         cart.CartItems.Should().BeEmpty();
//         cart.Closed.Should().BeFalse();
//     }
//
//     [Fact]
//     public void ShouldAddItemToCart()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 2;
//
//         cart.CartItems.Should().BeEmpty();
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.CartItems
//             .Should()
//             .ContainSingle(c =>
//                 c.ProductId.Value == productId &&
//                 c.Price == price &&
//                 c.Quantity.Value == quantity);
//     }
//
//     [Fact]
//     public void ShouldIncreaseQuantityWhenProductAlreadyExists()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 2;
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.CartItems
//             .Should()
//             .ContainSingle(c =>
//                 c.ProductId.Value == productId &&
//                 c.Quantity.Value == quantity);
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.CartItems
//             .Should()
//             .ContainSingle(c =>
//                 c.ProductId.Value == productId &&
//                 c.Quantity.Value == quantity + quantity);
//     }
//
//     [Fact]
//     public void ShouldDecreaseQuantityOfItem()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 2;
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.CartItems.Should().NotBeEmpty();
//
//         cart.RemoveItem(productId, false);
//
//         cart.CartItems
//             .Should()
//             .ContainSingle(c =>
//                 c.ProductId.Value == productId &&
//                 c.Quantity.Value == quantity - 1);
//     }
//
//     [Fact]
//     public void ShouldRemoveItemWhenQuantityIsZero()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 1;
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.CartItems.Should().NotBeEmpty();
//
//         cart.RemoveItem(productId, false);
//
//         cart.CartItems.Should().BeEmpty();
//     }
//
//     [Fact]
//     public void ShouldRemoveAllOfProduct()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 10;
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.CartItems.Should().NotBeEmpty();
//
//         cart.RemoveItem(productId, true);
//
//         cart.CartItems.Should().BeEmpty();
//     }
//
//     [Fact]
//     public void ShouldThrowErrorWhenRemovingInexistentItem()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 10;
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.CartItems.Should().NotBeEmpty();
//
//         var productToRemove = Guid.NewGuid();
//
//         var action = () => cart.RemoveItem(productToRemove, false);
//
//         action
//             .Should()
//             .Throw<DomainException>()
//             .WithMessage(string.Format(ShoppingCart.Errors.ItemNotPresentInCart,
//                 productToRemove.ToString()));
//     }
//
//     [Fact]
//     public void ShouldUpdateCartItems()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 10;
//
//         cart.AddItem(productId, price, quantity);
//
//         var productId2 = Guid.NewGuid();
//         var description2 = "def";
//         var price2 = 22.22M;
//         ushort quantity2 = 5;
//
//         cart.AddItem(productId2, price2, quantity2);
//
//         var newDescription = "abc2";
//         var newPrice = 33.33M;
//
//         cart.UpdateItems(productId, newPrice);
//
//         var itemUpdated = cart.CartItems.SingleOrDefault(ci => ci.ProductId.Value == productId);
//         var itemNotUpdated = cart.CartItems.SingleOrDefault(ci => ci.ProductId.Value == productId2);
//
//         itemUpdated.Price.Should().Be(newPrice);
//         itemNotUpdated.Price.Should().Be(price2);
//     }
//
//     [Fact]
//     public void ShouldThrowExceptionWhenUpdatingClosedCartItems()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 10;
//
//         cart.AddItem(productId, price, quantity);
//
//         var productId2 = Guid.NewGuid();
//         var description2 = "def";
//         var price2 = 22.22M;
//         ushort quantity2 = 5;
//
//         cart.AddItem(productId2, price2, quantity2);
//
//         var newDescription = "abc2";
//         var newPrice = 33.33M;
//
//         cart.Close();
//
//         var action = () => cart.UpdateItems(productId, newPrice);
//
//         action
//             .Should()
//             .Throw<DomainException>()
//             .WithMessage(string.Format(ShoppingCart.Errors.CantUpdateClosedCart,
//                 productId));
//     }
//
//     [Fact]
//     public void ShouldCloseCart()
//     {
//         var customerId = Guid.NewGuid();
//
//         var cart = new ShoppingCart(customerId);
//
//         var productId = Guid.NewGuid();
//         var description = "abc";
//         var price = 11.11M;
//         ushort quantity = 2;
//
//         cart.AddItem(productId, price, quantity);
//
//         cart.Close();
//
//         cart.Closed.Should().BeTrue();
//         var raisedEvent =
//             cart.Events.First(e => e.GetType().Equals(typeof(CartClosedDomainEvent))) as
//                 CartClosedDomainEvent;
//
//         raisedEvent.Should().NotBeNull();
//         raisedEvent?.ShoppingCart.Id.Should().Be(cart.Id);
//     }
// }