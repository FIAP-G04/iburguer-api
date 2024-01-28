using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders
{
    public interface ICompleteOrderUseCase
    {
        Task CompleteOrder(Guid orderId, CancellationToken cancellation);
    }
    public class CompleteOrderUseCase : ICompleteOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public CompleteOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task CompleteOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await Load(orderId, cancellation);
            order.Complete();

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
