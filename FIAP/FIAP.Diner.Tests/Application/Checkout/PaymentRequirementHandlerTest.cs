using FIAP.Diner.Application.Checkout.Requirement;
using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Tests.Application.Checkout;

public class PaymentRequirementHandlerTest
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IExternalPaymentService _externalPaymentService;

    private readonly PaymentRequirementHandler _manipulator;

    public PaymentRequirementHandlerTest()
    {
        _paymentRepository = Substitute.For<IPaymentRepository>();
        _externalPaymentService = Substitute.For<IExternalPaymentService>();

        _manipulator = new(_paymentRepository, _externalPaymentService);
    }

    [Fact]
    public async Task ShouldRequirePayment()
    {
        var orderId = Guid.NewGuid();
        var amount = 11.11M;

        var externalPaymentId = Guid.NewGuid().ToString();
        var qrCodeValue = Guid.NewGuid().ToString();

        var query = new RequirePaymentQuery(orderId, amount);

        _externalPaymentService.GenerateQRCode(amount).Returns((externalPaymentId, qrCodeValue));

        var result = await _manipulator.Handle(query, default);

        result.Should().NotBeNull();
        result.Amount.Should().Be(amount);
        result.QRCode.Value.Should().Be(qrCodeValue);
        result.QRCode.ExternalPaymentId.Should().Be(externalPaymentId);

        await _paymentRepository
            .Received()
            .Save(Arg.Is<Payment>(p =>
                p.OrderId == orderId &&
                p.Amount == amount &&
                p.QRCode.Value == qrCodeValue &&
                p.QRCode.ExternalPaymentId == externalPaymentId));
    }

    [Fact]
    public async Task ShouldThrowErrorWhenQRCodeGenerationNotSuccessful()
    {
        var orderId = Guid.NewGuid();
        var amount = 11.11M;

        var query = new RequirePaymentQuery(orderId, amount);

        _externalPaymentService.GenerateQRCode(amount).Returns((string.Empty, string.Empty));

        var action = async () => await _manipulator.Handle(query, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(string.Format(PaymentGenerationException.error, orderId));

        await _paymentRepository.DidNotReceiveWithAnyArgs().Save(Arg.Any<Payment>());
    }
}