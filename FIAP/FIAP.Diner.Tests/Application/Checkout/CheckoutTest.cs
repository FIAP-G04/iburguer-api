using FIAP.Diner.Application.Checkout;
using FIAP.Diner.Application.ShoppingCarts;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Tests.Application.Checkout
{
    public class CheckoutTest
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        private readonly CheckoutService _manipulator;

        public CheckoutTest()
        {
            _paymentRepository = Substitute.For<IPaymentRepository>();
            _shoppingCartRepository = Substitute.For<IShoppingCartRepository>();

            _manipulator = new(_paymentRepository, _shoppingCartRepository);
        }

        [Fact]
        public async Task ShouldCheckout()
        {
            var shoppingCart = ShoppingCart.GenerateAnonymousShoppingCart();
            shoppingCart.AddCartItem(Guid.NewGuid(), 11.11M, 1);

            _shoppingCartRepository.GetById(shoppingCart.Id, Arg.Any<CancellationToken>()).Returns(shoppingCart);

            await _manipulator.Checkout(shoppingCart.Id, default);

            await _paymentRepository.Received().Save(Arg.Is<Payment>(p =>
                p.ShoppingCart.Value == shoppingCart.Id.Value &&
                p.Confirmed == true),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldThrowErrorWhenShoppingCartNotFound()
        {
            var shoppingCartId = Guid.NewGuid();

            _shoppingCartRepository.GetById(shoppingCartId, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _manipulator.Checkout(shoppingCartId, default);

            await action.Should().ThrowAsync<ShoppingCartNotFoundException>()
                .WithMessage(string.Format(ShoppingCartNotFoundException.error, shoppingCartId));
        }
    }
}
