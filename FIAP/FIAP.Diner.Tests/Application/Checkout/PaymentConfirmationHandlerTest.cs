using FIAP.Diner.Application.Checkout.Confirmation;
using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Tests.Application.Checkout;

public class PaymentConfirmationHandlerTest
{
    private readonly IPaymentRepository _paymentRepository;

    private readonly PaymentConfirmationHandler _manipulator;

    public PaymentConfirmationHandlerTest()
    {
        _paymentRepository = Substitute.For<IPaymentRepository>();

        _manipulator = new(_paymentRepository);
    }

    [Fact]
    public async Task ShouldConfirmPayment()
    {
        var payment = new Payment(Guid.NewGuid(), 11.11M, new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        var command = new ConfirmPaymentCommand(payment.QRCode.ExternalPaymentId, DateTime.Now);

        _paymentRepository.Get(payment.QRCode.ExternalPaymentId).Returns(payment);

        await _manipulator.Handle(command, default);

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

        var command = new ConfirmPaymentCommand(Guid.NewGuid().ToString(), DateTime.Now);

        var action = async () => await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(CheckoutExceptions.PaymentDoesNotExist);

        await _paymentRepository.DidNotReceiveWithAnyArgs().Update(Arg.Any<Payment>());
    }

    [Fact]
    public async Task ShouldRefusePayment()
    {
        var payment = new Payment(Guid.NewGuid(), 11.11M, new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        var command = new RefusePaymentCommand(payment.QRCode.ExternalPaymentId);

        _paymentRepository.Get(payment.QRCode.ExternalPaymentId).Returns(payment);

        await _manipulator.Handle(command, default);

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

        var command = new RefusePaymentCommand(Guid.NewGuid().ToString());

        var action = async () => await _manipulator.Handle(command, default);

        await action.Should().ThrowAsync<DomainException>()
            .WithMessage(CheckoutExceptions.PaymentDoesNotExist);

        await _paymentRepository.DidNotReceiveWithAnyArgs().Update(Arg.Any<Payment>());
    }
}