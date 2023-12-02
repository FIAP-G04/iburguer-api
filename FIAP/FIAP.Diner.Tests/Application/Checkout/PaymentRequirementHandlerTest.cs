// using FIAP.Diner.Application.Checkout.Requirement;
// using FIAP.Diner.Domain.Abstractions;
//
// namespace FIAP.Diner.Tests.Application.Checkout;
//
// public class PaymentRequirementHandlerTest
// {
//     private readonly IExternalPaymentService _externalPaymentService;
//
//     private readonly PaymentRequirementHandler _manipulator;
//     private readonly IPaymentRepository _paymentRepository;
//
//     public PaymentRequirementHandlerTest()
//     {
//         _paymentRepository = Substitute.For<IPaymentRepository>();
//         _externalPaymentService = Substitute.For<IExternalPaymentService>();
//
//         _manipulator = new PaymentRequirementHandler(_paymentRepository, _externalPaymentService);
//     }
//
//     [Fact]
//     public async Task ShouldRequirePayment()
//     {
//         var cartId = Guid.NewGuid();
//         var amount = 11.11M;
//
//         var payment = new Payment(cartId, amount);
//
//         var externalPaymentId = Guid.NewGuid().ToString();
//         var qrCodeValue = Guid.NewGuid().ToString();
//
//         var query = new RequirePaymentQuery(cartId);
//
//         _externalPaymentService.GenerateQRCode(amount).Returns((externalPaymentId, qrCodeValue));
//         _paymentRepository.Get(query.CartId, Arg.Any<CancellationToken>()).Returns(payment);
//
//         var result = await _manipulator.Handle(query, default);
//
//         result.Should().NotBeNull();
//         result.Amount.Should().Be(amount);
//         result.QRCode.Should().Be(qrCodeValue);
//
//         await _paymentRepository
//             .Received()
//             .Update(Arg.Is<Payment>(p =>
//                 p.CartId == cartId &&
//                 p.Amount == amount &&
//                 p.QRCode.Value == qrCodeValue &&
//                 p.QRCode.ExternalPaymentId == externalPaymentId), Arg.Any<CancellationToken>());
//     }
//
//     [Fact]
//     public async Task ShouldThrowErrorWhenQRCodeGenerationNotSuccessful()
//     {
//         var cartId = Guid.NewGuid();
//         var amount = 11.11M;
//
//         var payment = new Payment(cartId, amount);
//
//         var query = new RequirePaymentQuery(cartId);
//
//         _paymentRepository.Get(query.CartId, Arg.Any<CancellationToken>()).Returns(payment);
//
//         _externalPaymentService.GenerateQRCode(amount).Returns((string.Empty, string.Empty));
//
//         var action = async () => await _manipulator.Handle(query, default);
//
//         await action.Should().ThrowAsync<DomainException>()
//             .WithMessage(string.Format(PaymentGenerationException.error, cartId));
//
//         await _paymentRepository.DidNotReceiveWithAnyArgs()
//             .Update(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
//     }
// }