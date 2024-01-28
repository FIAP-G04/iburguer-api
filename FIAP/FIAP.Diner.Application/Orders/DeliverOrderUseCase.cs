using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders
{
    public interface IDeliverOrderUseCase
    {
        Task DeliverOrder(Guid orderId, CancellationToken cancellation);
    }

    public class DeliverOrderUseCase : IDeliverOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public DeliverOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task DeliverOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await Load(orderId, cancellation);
            order.Deliver();

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
