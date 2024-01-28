using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders
{
    public interface IConfirmOrderUseCase
    {
        Task ConfirmOrder(Guid shoppingCarId, CancellationToken cancellation);
    }

    public class ConfirmOrderUseCase : IConfirmOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public ConfirmOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task ConfirmOrder(Guid shoppingCarId, CancellationToken cancellation)
        {
            var order = await _repository.GetOrderByShoppingCartId(shoppingCarId, cancellation);

            if (order is null)
                throw new OrderNotFoundException(shoppingCarId);
            order.Confirm();

            await _repository.Update(order, cancellation);
        }
    }
}
