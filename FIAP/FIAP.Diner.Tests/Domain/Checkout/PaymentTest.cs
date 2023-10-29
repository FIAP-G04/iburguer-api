namespace FIAP.Diner.Tests.Domain.Checkout;

public class PaymentTest
{
    [Fact]
    public void ShouldCreatePayment()
    {
        var cartId = Guid.NewGuid();
        var amount = 11.11M;
        var qrCode = new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var payment = new Payment(cartId, amount);

        payment.Id.Should().NotBeNull();
        payment.Id.Value.Should().NotBe(Guid.Empty);
        payment.Status.Should().Be(PaymentStatus.Processing);
        payment.Amount.Should().Be(amount);
        payment.CartId.Should().Be(cartId);
        payment.QRCode.Should().BeNull();
        payment.Confirmed.Should().BeFalse();
        payment.PayedAt.Should().BeNull();
    }

    [Fact]
    public void ShouldAddQRCode()
    {
        var cartId = Guid.NewGuid();
        var amount = 11.11M;
        var qrCode = new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var payment = new Payment(cartId, amount);

        payment.AddQRCode(qrCode);

        payment.QRCode.Should().Be(qrCode);
    }

    [Fact]
    public void ShouldConfirmPayment()
    {
        var cartId = Guid.NewGuid();
        var amount = 11.11M;
        var qrCode = new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var payment = new Payment(cartId, amount);

        var payedAt = DateTime.UtcNow;
        payment.Confirm(payedAt);

        payment.Status.Should().Be(PaymentStatus.Confirmed);
        payment.PayedAt.Should().Be(payedAt);
        payment.Confirmed.Should().BeTrue();

        payment.Events.Should().HaveCount(1);
        var raisedEvent =
            payment.Events.First(e => e.GetType().Equals(typeof(PaymentConfirmedDomainEvent))) as
                PaymentConfirmedDomainEvent;

        raisedEvent.Should().NotBeNull();
        raisedEvent?.CartId.Should().Be(cartId);
    }

    [Fact]
    public void ShouldRefusePayment()
    {
        var cartId = Guid.NewGuid();
        var amount = 11.11M;
        var qrCode = new QRCode(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var payment = new Payment(cartId, amount);

        payment.Refuse();

        payment.Status.Should().Be(PaymentStatus.Refused);
        payment.PayedAt.Should().BeNull();
        payment.Confirmed.Should().BeFalse();

        payment.Events.Should().HaveCount(1);
        var raisedEvent =
            payment.Events.First(e => e.GetType().Equals(typeof(PaymentRefusedDomainEvent))) as
                PaymentRefusedDomainEvent;

        raisedEvent.Should().NotBeNull();
        raisedEvent?.OrderId.Should().Be(cartId);
    }
}