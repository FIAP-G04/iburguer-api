namespace FIAP.Diner.Tests.Domain.Checkout;

public class PaymentTest
{
    [Fact]
    public void ShouldCreatePayment()
    {
        var orderId = Guid.NewGuid();
        var amount = 11.11M;
        var qrCode = new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var payment = new Payment(orderId, amount, qrCode);

        payment.Status.Should().Be(PaymentStatus.Processing);
        payment.Amount.Should().Be(amount);
        payment.OrderId.Should().Be(orderId);
        payment.QRCode.Should().Be(qrCode);
        payment.Confirmed.Should().BeFalse();
        payment.PayedAt.Should().BeNull();
    }

    [Fact]
    public void ShouldConfirmPayment()
    {
        var orderId = Guid.NewGuid();
        var amount = 11.11M;
        var qrCode = new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var payment = new Payment(orderId, amount, qrCode);

        var payedAt = DateTime.UtcNow;
        payment.Confirm(payedAt);

        payment.Status.Should().Be(PaymentStatus.Confirmed);
        payment.PayedAt.Should().Be(payedAt);
        payment.Confirmed.Should().BeTrue();

        payment.Events.Should().HaveCount(1);
        var raisedEvent = payment.Events.First(e => e.GetType().Equals(typeof(PaymentConfirmedDomainEvent))) as PaymentConfirmedDomainEvent;

        raisedEvent.Should().NotBeNull();
        raisedEvent?.OrderId.Should().Be(orderId);
    }

    [Fact]
    public void ShouldRefusePayment()
    {
        var orderId = Guid.NewGuid();
        var amount = 11.11M;
        var qrCode = new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var payment = new Payment(orderId, amount, qrCode);

        payment.Refuse();

        payment.Status.Should().Be(PaymentStatus.Refused);
        payment.PayedAt.Should().BeNull();
        payment.Confirmed.Should().BeFalse();

        payment.Events.Should().HaveCount(1);
        var raisedEvent = payment.Events.First(e => e.GetType().Equals(typeof(PaymentRefusedDomainEvent))) as PaymentRefusedDomainEvent;

        raisedEvent.Should().NotBeNull();
        raisedEvent?.OrderId.Should().Be(orderId);
    }
}