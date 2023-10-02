namespace FIAP.Diner.Tests.Domain.Checkout;

public class PaymentDomainServiceTest
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IExternalPaymentService _externalPaymentService;

    private readonly PaymentDomainService _manipulator;

    public PaymentDomainServiceTest()
    {
        _paymentRepository = Substitute.For<IPaymentRepository>();
        _externalPaymentService = Substitute.For<IExternalPaymentService>();

        _manipulator = new(_paymentRepository, _externalPaymentService);
    }

    [Fact]
    public async Task ShouldRequirePayment()
    {
        var orderId = Guid.NewGuid();
        var amount = 11.11;

        var externalPaymentId = Guid.NewGuid().ToString();
        var qrCodeValue = Guid.NewGuid().ToString();

        _externalPaymentService.GenerateQRCode(amount).Returns((externalPaymentId, qrCodeValue));

        var result = await _manipulator.RequirePayment(orderId, amount);

        result.Should().NotBeNull();
        result.OrderId.Should().Be(orderId);
        result.Amount.Should().Be(amount);
        result.QRCode.Value.Should().Be(qrCodeValue);
        result.QRCode.ExternalPaymentId.Should().Be(externalPaymentId);

        await _paymentRepository.Received().Save(result);
    }

    [Fact]
    public async Task ShouldThrowErrorWhenQRCodeGenerationNotSuccessful()
    {
        var orderId = Guid.NewGuid();
        var amount = 11.11;

        _externalPaymentService.GenerateQRCode(amount).Returns((string.Empty, string.Empty));

        var action = async () => await _manipulator.RequirePayment(orderId, amount);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(CheckoutExceptions.ErrorGeneratingPayment);

        await _paymentRepository.DidNotReceiveWithAnyArgs().Save(Arg.Any<Payment>());
    }

    [Fact]
    public async Task ShouldConfirmPayment()
    {
        var payment = new Payment(Guid.NewGuid(), 11.11, new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
    
        _paymentRepository.Get(payment.QRCode.ExternalPaymentId).Returns(payment);
    
        await _manipulator.ConfirmPayment(payment.QRCode.ExternalPaymentId, DateTime.Now);
    
        await _paymentRepository
            .Received()
            .Update(Arg.Is<Payment>(p =>
                p.Id == payment.Id &&
                p.Status == PaymentStatus.Confirmed));
    }

    [Fact]
    public async Task ShouldThrowErrorWhenConfirmedPaymentNotFound()
    {
        _paymentRepository.Get(Arg.Any<string>()).ReturnsNull();

        var action = async () => await _manipulator.ConfirmPayment(Guid.NewGuid().ToString(), DateTime.Now);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(CheckoutExceptions.PaymentDoesNotExist);

        await _paymentRepository.DidNotReceiveWithAnyArgs().Update(Arg.Any<Payment>());
    }

    [Fact]
    public async Task ShouldRefusePayment()
    {
        var payment = new Payment(Guid.NewGuid(), 11.11, new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
    
        _paymentRepository.Get(payment.QRCode.ExternalPaymentId).Returns(payment);
    
        await _manipulator.RefusePayment(payment.QRCode.ExternalPaymentId);
    
        await _paymentRepository
            .Received()
            .Update(Arg.Is<Payment>(p =>
                p.Id == payment.Id &&
                p.Status == PaymentStatus.Refused));
    }

    [Fact]
    public async Task ShouldThrowErrorWhenRefusedPaymentNotFound()
    {
        _paymentRepository.Get(Arg.Any<string>()).ReturnsNull();

        var action = async () => await _manipulator.RefusePayment(Guid.NewGuid().ToString());

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(CheckoutExceptions.PaymentDoesNotExist);

        await _paymentRepository.DidNotReceiveWithAnyArgs().Update(Arg.Any<Payment>());
    }
}