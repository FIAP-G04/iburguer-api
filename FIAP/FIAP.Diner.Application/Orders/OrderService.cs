using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IOrderRepository));

        _repository = repository;
    }

    public async Task RegisterOrder(Guid shoppingCartId, CancellationToken cancellation)
    {
        var number = await _repository.GenerateOrderNumber();
        var order = Order.Create(number, shoppingCartId);

        await _repository.Save(order, cancellation);
    }

    public async Task ConfirmOrder(Guid orderId, CancellationToken cancellation)
    {
        var order = await Load(orderId, cancellation);

        order.Confirm();

        await _repository.Update(order, cancellation);
    }

    public async Task StartOrder(Guid orderId, CancellationToken cancellation)
    {
        var order = await Load(orderId, cancellation);

        order.Start();

        await _repository.Update(order, cancellation);
    }

    public async Task CompleteOrder(Guid orderId, CancellationToken cancellation)
    {
        var order = await Load(orderId, cancellation);

        order.Complete();

        await _repository.Update(order, cancellation);
    }

    public async Task DeliverOrder(Guid orderId, CancellationToken cancellation)
    {
        var order = await Load(orderId, cancellation);

        order.Deliver();

        await _repository.Update(order, cancellation);
    }

    public async Task CancelOrder(Guid orderId, CancellationToken cancellation)
    {
        var order = await Load(orderId, cancellation);

        order.Cancel();

        await _repository.Update(order, cancellation);
    }

    private async Task<Order> Load(Guid orderId, CancellationToken cancellation)
    {
        var order = await _repository.GetById(orderId, cancellation);

        if(order is null)
            throw new OrderNotFoundException(orderId);

        return order;
    }
}