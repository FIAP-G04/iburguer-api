using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Order.ConsultOrder;
using FIAP.Diner.Application.Order.Tracking;
using FIAP.Diner.Domain.Order;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public OrderController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> ConsultOrder(Guid customerId)
        {
            var query = new ConsultOrderQuery(customerId);
            var result = await _queryDispatcher.Dispatch<ConsultOrderQuery, OrderDetails>(query, default);
            return Ok(result);
        }

        [HttpGet]
        [Route("queue")]
        public async Task<IActionResult> GetOrderQueue()
        {
            var result = await _queryDispatcher.Dispatch<GetOrderQueueQuery, IEnumerable<OrderDetails>>(new GetOrderQueueQuery(), default);
            return Ok(result);
        }

        [HttpPut]
        [Route("order")]
        public async Task<IActionResult> UpdateOrderTracking(UpdateOrderTrackingCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }
    }
}
