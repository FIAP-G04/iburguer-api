using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Order;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> Get(Guid id);

    Task<OrderDetails> GetDetails(Guid id);
    Task Save(Order order);
    Task Update(Order order);
}