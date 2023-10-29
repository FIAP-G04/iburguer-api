using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Cart;
using FIAP.Diner.Domain.Cart;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public CartController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [Route("product")]
    public async Task<IActionResult> AddItem([FromBody] AddItemToCartCommand command)
    {
        await _commandDispatcher.Dispatch(command, default);
        return Ok();
    }

    [HttpDelete]
    [Route("product")]
    public async Task<IActionResult> Remove([FromBody] RemoveItemFromCartCommand command)
    {
        await _commandDispatcher.Dispatch(command, default);
        return Ok();
    }

    [HttpPost]
    [Route("close")]
    public async Task<IActionResult> CloseCart([FromBody] CloseCartCommand command)
    {
        await _commandDispatcher.Dispatch(command, default);
        return Ok();
    }

    [HttpGet]
    [Route("{customerId}")]
    public async Task<IActionResult> GetItems(Guid customerId)
    {
        var query = new GetCartItemsQuery(customerId);
        var result =
            await _queryDispatcher.Dispatch<GetCartItemsQuery, CartDetails>(query, default);
        return Ok(result);
    }
}