using FIAP.Diner.Application.Checkout.Confirmation;
using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Tests.Application.Checkout;

public class PaymentConfirmationHandlerTest
{
    private readonly PaymentConfirmationHandler _manipulator;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentConfirmationHandlerTest()
    {
        _paymentRepository = Substitute.For<IPaymentRepository>();

        _manipulator = new PaymentConfirmationHandler(_paymentRepository);
    }

    [Fact]
    public async Task ShouldConfirmPayment()
    {
        var payment = new Payment(Guid.NewGuid(), 11.11M);
        payment.AddQRCode(new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        var command = new ConfirmPaymentCommand(payment.QRCode.ExternalPaymentId, DateTime.Now);

        _paymentRepository.Get(payment.QRCode.ExternalPaymentId, Arg.Any<CancellationToken>())
            .Returns(payment);

        await _manipulator.Handle(command, default);

        await _paymentRepository
            .Received()
            .Update(Arg.Is<Payment>(p =>
                p.Id == payment.Id &&
                p.Status == PaymentStatus.Confirmed), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldThrowErrorWhenConfirmedPaymentNotFound()
    {
        _paymentRepository.Get(Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsNull();

        var command = new ConfirmPaymentCommand(Guid.NewGuid().ToString(), DateTime.Now);

        var action = async () => await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(string.Format(PaymentNotExistsException.error,
                command.ExternalPaymentServiceId));

        await _paymentRepository.DidNotReceiveWithAnyArgs()
            .Update(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldRefusePayment()
    {
        var payment = new Payment(Guid.NewGuid(), 11.11M);
        payment.AddQRCode(new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        var command = new RefusePaymentCommand(payment.QRCode.ExternalPaymentId);

        _paymentRepository.Get(payment.QRCode.ExternalPaymentId, Arg.Any<CancellationToken>())
            .Returns(payment);

        await _manipulator.Handle(command, default);

        await _paymentRepository
            .Received()
            .Update(Arg.Is<Payment>(p =>
                p.Id == payment.Id &&
                p.Status == PaymentStatus.Refused), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldThrowErrorWhenRefusedPaymentNotFound()
    {
        _paymentRepository.Get(Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsNull();

        var command = new RefusePaymentCommand(Guid.NewGuid().ToString());

        var action = async () => await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(string.Format(PaymentNotExistsException.error,
                command.ExternalPaymentServiceId));

        await _paymentRepository.DidNotReceiveWithAnyArgs()
            .Update(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
    }
}