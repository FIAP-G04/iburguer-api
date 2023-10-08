using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Tracking;

public interface IOrderRepository : IRepository<OrderTracking>
{
    Task<OrderTracking> GetByOrderId(Guid orderId);
    Task Save(OrderTracking orderTracking);
    Task Update(OrderTracking orderTracking);
}