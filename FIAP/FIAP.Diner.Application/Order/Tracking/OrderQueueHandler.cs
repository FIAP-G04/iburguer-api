using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Application.Order.Tracking;

public class OrderQueueHandler : IQueryHandler<GetOrderQueueQuery, IEnumerable<OrderDetails>>
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueueHandler(IOrderRepository orderRepository) =>
        _orderRepository = orderRepository;

    public async Task<IEnumerable<OrderDetails>> Handle(GetOrderQueueQuery query,
        CancellationToken cancellation)
        => await _orderRepository.GetQueue(cancellation);
}