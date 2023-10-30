using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Orders;
using FIAP.Diner.Domain.Orders;
using FIAP.Diner.Infrastructure.Data.Modules.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IOrderRetriever _retriever;

    public OrderController(IOrderService orderService, IOrderRetriever retriever)
    {
        _orderService = orderService;
        _retriever = retriever;
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedOrders(CancellationToken cancellation, int page = 1,
        int limit = 10)
    {
        return Ok(await _retriever.GetPagedOrdersAsync(page, limit, cancellation));
    }

    [HttpGet]
    [Route("{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken cancellation)
    {

        return Ok();
    }

    [HttpPatch]
    [Route("{orderId}/start")]
    public async Task<IActionResult> StartOrder(Guid orderId, CancellationToken cancellation)
    {
        await _orderService.StartOrder(orderId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{orderId}/complete")]
    public async Task<IActionResult> CompleteOrder(Guid orderId, CancellationToken cancellation)
    {
        await _orderService.CompleteOrder(orderId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{orderId}/deliver")]
    public async Task<IActionResult> DeliverOrder(Guid orderId, CancellationToken cancellation)
    {
        await _orderService.DeliverOrder(orderId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid orderId, CancellationToken cancellation)
    {
        await _orderService.CancelOrder(orderId, cancellation);
        return Ok();
    }
}