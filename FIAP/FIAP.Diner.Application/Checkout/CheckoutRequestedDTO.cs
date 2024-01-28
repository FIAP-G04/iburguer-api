namespace FIAP.Diner.Application.Checkout
{
    public record CheckoutRequestedDTO(int OrderNumber, Guid PaymentId);
}
