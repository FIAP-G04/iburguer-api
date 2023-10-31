using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Orders;

public interface IOrderRepository : IRepository<Orders.Order>
{
    Task Save(Order order, CancellationToken cancellation);

    Task Update(Order order, CancellationToken cancellation);

    Task<Order?> GetById(OrderId orderId, CancellationToken cancellation);

    Task<int> GenerateOrderNumber(CancellationToken cancellation);

    Task<Order?> GetOrderByShoppingCartId(Guid shoppingCartId, CancellationToken cancellation);
}