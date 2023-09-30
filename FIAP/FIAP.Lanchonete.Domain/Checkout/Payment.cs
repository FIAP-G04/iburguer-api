using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Checkout
{
    public class Payment : Entity<Guid>, IAggregateRoot
    {
        public Guid OrderId { get; private set; }
        public double Amount { get; private set; }
        public DateTime? PayedAt { get; private set; }
        public QRCode QRCode { get; private set; }
        public PaymentStatus Status { get; private set; }
        public bool Confirmed => Status is PaymentStatus.Confirmed && PayedAt is not null;

        public Payment(Guid orderId, double amount, QRCode qrCode)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Amount = amount;
            QRCode = qrCode;
            Status = PaymentStatus.Processing;
        }

        public void Confirm(DateTime payedAt)
        {
            PayedAt = payedAt;
            Status = PaymentStatus.Confirmed;

            RaiseEvent(new PaymentConfirmedDomainEvent(OrderId));
        }

        public void Refuse()
        {
            Status = PaymentStatus.Refused;

            RaiseEvent(new PaymentRefusedDomainEvent(OrderId));
        }
    }
}
