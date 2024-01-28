using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders
{
    public interface ICancelOrderUseCase
    {
        Task CancelOrder(Guid orderId, CancellationToken cancellation);
    }

    public class CancelOrderUseCase : ICancelOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public CancelOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task CancelOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await Load(orderId, cancellation);
            order.Cancel();

            await _repository.Update(order, cancellation);
        }

        private async Task<Order> Load(Guid orderId, CancellationToken cancellation)
        {
            var order = await _repository.GetById(orderId, cancellation);

            if (order is null)
                throw new OrderNotFoundException(orderId);

            return order;
        }
    }
}
