namespace FIAP.Diner.Application.Orders;

public interface IOrderService
{
    Task RegisterOrder(Guid shoppingCartId, CancellationToken cancellation);

    Task ConfirmOrder(Guid orderId, CancellationToken cancellation);

    Task StartOrder(Guid orderId, CancellationToken cancellation);

    Task CompleteOrder(Guid orderId, CancellationToken cancellation);

    Task DeliverOrder(Guid orderId, CancellationToken cancellation);

    Task CancelOrder(Guid orderId, CancellationToken cancellation);
}