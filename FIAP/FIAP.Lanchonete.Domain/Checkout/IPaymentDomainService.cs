namespace FIAP.Diner.Domain.Checkout
{
    public interface IPaymentDomainService
    {
        Task<Payment> RequirePayment(Guid orderId, double amount);

        Task ConfirmPayment(string externalPaymentServiceId, DateTime payedAt);

        Task RefusePayment(string externalPaymentServiceId);
    }
}
