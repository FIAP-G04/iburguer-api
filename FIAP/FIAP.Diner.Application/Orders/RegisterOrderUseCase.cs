using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders
{
    public interface IRegisterOrderUseCase
    {
        Task<int> RegisterOrder(Guid shoppingCartId, CancellationToken cancellation);
    }

    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public RegisterOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task<int> RegisterOrder(Guid shoppingCartId, CancellationToken cancellation)
        {
            var number = await _repository.GenerateOrderNumber(cancellation);
            var order = Order.Create(number, shoppingCartId);

            await _repository.Save(order, cancellation);

            return number;
        }
    }
}
