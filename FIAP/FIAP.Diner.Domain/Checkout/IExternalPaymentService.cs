namespace FIAP.Diner.Domain.Checkout;

public interface IExternalPaymentService
{
    Task<(string, string)> GenerateQRCode(decimal amount);
}