using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders
{
    public interface IStartOrderUseCase
    {
        Task StartOrder(Guid orderId, CancellationToken cancellation);
    }

    public class StartOrderUseCase : IStartOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public StartOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task StartOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await Load(orderId, cancellation);
            order.Start();

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
