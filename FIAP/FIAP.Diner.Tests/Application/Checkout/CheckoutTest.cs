using FIAP.Diner.Application.Checkout;
using FIAP.Diner.Application.Orders;
using FIAP.Diner.Application.ShoppingCarts;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Tests.Application.Checkout
{
    public class CheckoutTest
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IRegisterOrderUseCase _registerOrderUseCase;

        private readonly ICheckoutUseCase _checkoutUseCase;
        private readonly IGetPaymentStatusUseCase _getPaymentStatusUseCase;
        private readonly IConfirmPaymentUseCase _confirmPaymentUseCase;
        private readonly IRefusePaymentUseCase _refusePaymentUseCase;


        public CheckoutTest()
        {
            _paymentRepository = Substitute.For<IPaymentRepository>();
            _shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            _registerOrderUseCase = Substitute.For<IRegisterOrderUseCase>();

            _checkoutUseCase = new CheckoutUseCase(_paymentRepository, _shoppingCartRepository, _registerOrderUseCase);
            _getPaymentStatusUseCase = new GetPaymentStatusUseCase(_paymentRepository);
            _confirmPaymentUseCase = new ConfirmPaymentUseCase(_paymentRepository);
            _refusePaymentUseCase = new RefusePaymentUseCase(_paymentRepository);
        }

        [Fact]
        public async Task ShouldCheckout()
        {
            var shoppingCart = ShoppingCart.GenerateAnonymousShoppingCart();
            shoppingCart.AddCartItem(Guid.NewGuid(), 11.11M, 1);

            _shoppingCartRepository.GetById(shoppingCart.Id, Arg.Any<CancellationToken>()).Returns(shoppingCart);

            var result = await _checkoutUseCase.Checkout(shoppingCart.Id, default);

            result.Number.Should().BePositive();

            await _paymentRepository.Received().Save(Arg.Is<Payment>(p =>
                p.ShoppingCart.Value == shoppingCart.Id.Value &&
                p.Confirmed == false),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldThrowErrorWhenShoppingCartNotFound()
        {
            var shoppingCartId = Guid.NewGuid();

            _shoppingCartRepository.GetById(shoppingCartId, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _checkoutUseCase.Checkout(shoppingCartId, default);

            await action.Should().ThrowAsync<ShoppingCartNotFoundException>()
                .WithMessage(string.Format(ShoppingCartNotFoundException.error, shoppingCartId));
        }

        [Fact]
        public async Task ShouldGetPaymentStatus()
        {
            var payment = new Payment(Guid.NewGuid(), 11.11M);
            payment.Confirm();

            _paymentRepository.GetById(payment.Id, Arg.Any<CancellationToken>()).Returns(payment);

            var result = await _getPaymentStatusUseCase.GetPaymentStatus(payment.Id, default);

            result.Should().NotBeNull();
            result.PaymentId.Should().Be(payment.Id.Value);
            result.Status.Should().Be(payment.Status);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenPaymentStatusToFindDoesNotExist()
        {
            var paymentId = Guid.NewGuid();

            _paymentRepository.GetById(paymentId, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _getPaymentStatusUseCase.GetPaymentStatus(paymentId, default);

            await action.Should().ThrowAsync<PaymentNotFoundException>()
                .WithMessage(string.Format(PaymentNotFoundException.error, paymentId));
        }

        [Fact]
        public async Task ShouldConfirmPayment()
        {
            var payment = new Payment(Guid.NewGuid(), 11.11M);
            _paymentRepository.GetById(payment.Id, Arg.Any<CancellationToken>()).Returns(payment);

            await _confirmPaymentUseCase.ConfirmPayment(payment.Id, default);

            await _paymentRepository
                .Received()
                .Update(Arg.Is<Payment>(p => p.Id == payment.Id &&
                    p.Status == PaymentStatus.Received), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldThrowErrorWhenConfirmingPaymentToFindDoesNotExist()
        {
            var paymentId = Guid.NewGuid();

            _paymentRepository.GetById(paymentId, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _confirmPaymentUseCase.ConfirmPayment(paymentId, default);

            await action.Should().ThrowAsync<PaymentNotFoundException>()
                .WithMessage(string.Format(PaymentNotFoundException.error, paymentId));
        }

        [Fact]
        public async Task ShouldRefusePayment()
        {
            var payment = new Payment(Guid.NewGuid(), 11.11M);
            _paymentRepository.GetById(payment.Id, Arg.Any<CancellationToken>()).Returns(payment);

            await _refusePaymentUseCase.RefusePayment(payment.Id, default);

            await _paymentRepository
                .Received()
                .Update(Arg.Is<Payment>(p => p.Id == payment.Id &&
                    p.Status == PaymentStatus.Refused), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldThrowErrorWhenRefusingPaymentToFindDoesNotExist()
        {
            var paymentId = Guid.NewGuid();

            _paymentRepository.GetById(paymentId, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = async () => await _refusePaymentUseCase.RefusePayment(paymentId, default);

            await action.Should().ThrowAsync<PaymentNotFoundException>()
                .WithMessage(string.Format(PaymentNotFoundException.error, paymentId));
        }
    }
}
