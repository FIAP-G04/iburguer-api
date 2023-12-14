namespace FIAP.Diner.Application.Checkout;

public interface ICheckoutService
{
    Task<NewPaymentDTO> Checkout(Guid shoppingCartId, CancellationToken cancellation);

    Task ConfirmPayment(Guid paymentId, CancellationToken cancellation);

    Task RefusePayment(Guid paymentId, CancellationToken cancellation);

    Task<PaymentStatusDTO> GetPaymentStatus(Guid paymentId, CancellationToken cancellation);
}