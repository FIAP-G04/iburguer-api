using FIAP.Diner.Application.Orders;
using FIAP.Diner.Infrastructure.Data.Modules.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IStartOrderUseCase _startOrderUseCase;
    private readonly ICompleteOrderUseCase _completeOrderUseCase;
    private readonly IDeliverOrderUseCase _deliverOrderUseCase;
    private readonly ICancelOrderUseCase _cancelOrderUseCase;
    private readonly IOrderRetriever _retriever;

    public OrderController(
        IOrderRetriever retriever,
        IStartOrderUseCase startOrderUseCase,
        ICompleteOrderUseCase completeOrderUseCase,
        IDeliverOrderUseCase deliverOrderUseCase,
        ICancelOrderUseCase cancelOrderUseCase)
    {
        _retriever = retriever;
        _startOrderUseCase = startOrderUseCase;
        _completeOrderUseCase = completeOrderUseCase;
        _deliverOrderUseCase = deliverOrderUseCase;
        _cancelOrderUseCase = cancelOrderUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedOrders(CancellationToken cancellation, int page = 1,
        int limit = 10)
    {
        return Ok(await _retriever.GetPagedOrdersAsync(page, limit, cancellation));
    }

    [HttpGet]
    [Route("queue")]
    public async Task<IActionResult> GetOrderQueueAsync(CancellationToken cancellation, int page = 1,
        int limit = 10)
    {
        return Ok(await _retriever.GetOrderQueueAsync(page, limit, cancellation));
    }

    [HttpPatch]
    [Route("{orderId}/start")]
    public async Task<IActionResult> StartOrder(Guid orderId, CancellationToken cancellation)
    {
        await _startOrderUseCase.StartOrder(orderId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{orderId}/complete")]
    public async Task<IActionResult> CompleteOrder(Guid orderId, CancellationToken cancellation)
    {
        await _completeOrderUseCase.CompleteOrder(orderId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{orderId}/deliver")]
    public async Task<IActionResult> DeliverOrder(Guid orderId, CancellationToken cancellation)
    {
        await _deliverOrderUseCase.DeliverOrder(orderId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid orderId, CancellationToken cancellation)
    {
        await _cancelOrderUseCase.CancelOrder(orderId, cancellation);
        return Ok();
    }
}