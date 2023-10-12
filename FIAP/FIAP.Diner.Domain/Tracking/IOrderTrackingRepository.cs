using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Tracking;

public interface IOrderTrackingRepository : IRepository<OrderTracking>
{
    Task<OrderTracking> GetByOrderId(Guid orderId);
    Task Save(OrderTracking orderTracking);
    Task Update(OrderTracking orderTracking);
}