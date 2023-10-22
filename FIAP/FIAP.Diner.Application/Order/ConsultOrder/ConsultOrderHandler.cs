using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Application.Order.ConsultOrder
{
    public class ConsultOrderHandler : IQueryHandler<ConsultOrderQuery, OrderDetails>
    {
        private readonly IOrderRepository _orderRepository;

        public ConsultOrderHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<OrderDetails> Handle(ConsultOrderQuery query, CancellationToken cancellation)
            => await _orderRepository.GetDetails(query.OrderId)
                ?? throw new OrderNotFoundException(query.OrderId);
    }
}
