namespace FIAP.Diner.Application.Checkout;

public interface ICheckoutService
{
    Task Checkout(Guid shoppingCartId, CancellationToken cancellation);
}