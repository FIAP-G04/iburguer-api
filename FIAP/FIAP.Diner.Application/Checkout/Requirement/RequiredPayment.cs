using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout.Requirement;

public class RequiredPayment
{
    public RequiredPayment(Payment payment)
    {
        Id = payment.Id.Value;
        Amount = payment.Amount;
        QRCode = payment.QRCode.Value;
    }

    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public string QRCode { get; private set; }
}