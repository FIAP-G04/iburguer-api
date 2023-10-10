using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout.Requirement;

public class RequiredPayment
{
    public PaymentId Id { get; private set; }
    public decimal Amount { get; private set; }
    public QRCode QRCode { get; private set; }

    public RequiredPayment(Payment payment)
    {
        Id = payment.Id;
        Amount = payment.Amount;
        QRCode = payment.QRCode;
    }
}