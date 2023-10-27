using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Checkout.Confirmation;
using FIAP.Diner.Application.Checkout.Requirement;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CheckoutController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("{cartId}")]
        public async Task<IActionResult> RequirePayment(Guid cartId)
        {
            var query = new RequirePaymentQuery(cartId);
            var result = await _queryDispatcher.Dispatch<RequirePaymentQuery, RequiredPayment>(query, default);
            return Ok(result);
        }

        [HttpPut]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmPayment(ConfirmPaymentCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }

        [HttpPut]
        [Route("refuse")]
        public async Task<IActionResult> RefusePayment(RefusePaymentCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }
    }
}
