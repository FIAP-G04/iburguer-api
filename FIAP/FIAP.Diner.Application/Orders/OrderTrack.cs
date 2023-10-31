using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders;

public class OrderTrack
{
    private readonly IOrderRepository _repository;

    public OrderTrack(IOrderRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IOrderRepository));

        _repository = repository;
    }

    public async Task RegisterOrder(Guid shoppingCartId, CancellationToken cancellation)
    {
        var number = await _repository.GenerateOrderNumber(cancellation);
        var order = Order.Create(number, shoppingCartId);

        await _repository.Save(order, cancellation);
    }
}