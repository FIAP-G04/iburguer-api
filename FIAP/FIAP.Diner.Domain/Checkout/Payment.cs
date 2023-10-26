using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Checkout
{
    public class Payment : Entity<PaymentId>, IAggregateRoot
    {
        public Guid CartId { get; }
        public decimal Amount { get; private set; }
        public DateTime? PayedAt { get; private set; }
        public QRCode? QRCode { get; private set; }
        public PaymentStatus Status { get; private set; }
        public bool Confirmed => Status is PaymentStatus.Confirmed && PayedAt is not null;

        public Payment(Guid orderId, decimal amount)
        {
            Id = Guid.NewGuid();
            CartId = orderId;
            Amount = amount;
            QRCode = null;
            Status = PaymentStatus.Processing;
        }

        public void AddQRCode(QRCode qrCode)
            => QRCode = qrCode;

        public void Confirm(DateTime payedAt)
        {
            PayedAt = payedAt;
            Status = PaymentStatus.Confirmed;

            RaiseEvent(new PaymentConfirmedDomainEvent(CartId));
        }

        public void Refuse()
        {
            Status = PaymentStatus.Refused;

            RaiseEvent(new PaymentRefusedDomainEvent(CartId));
        }
    }
}
