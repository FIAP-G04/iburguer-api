using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Order;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> Get(Guid id, CancellationToken cancellation);

    Task<OrderDetails> GetDetails(Guid id, CancellationToken cancellation);

    Task<IEnumerable<OrderDetails>> GetQueue(CancellationToken cancellation);

    Task<string> GetNextWithdrawCode(CancellationToken cancellation);
    Task Save(Order order, CancellationToken cancellation);
    Task Update(Order order, CancellationToken cancellation);
}