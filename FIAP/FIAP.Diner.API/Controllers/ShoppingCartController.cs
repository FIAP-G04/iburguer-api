using FIAP.Diner.Application.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/carts")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCart _shoppingCart;

    public ShoppingCartController(IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShoppingCart(CreateShoppingCartDTO dto, CancellationToken cancellation)
    {
        if (dto.CustomerId == null)
        {
            var shoppingCartId = await _shoppingCart.CreateAnonymousShoppingCart(cancellation);

            return Ok(shoppingCartId);
        }
        else
        {
            var shoppingCartId = await _shoppingCart.CreateCustomerShoppingCart(dto.CustomerId.Value, cancellation);

            return Ok(shoppingCartId);
        }
    }

    [HttpPatch]
    [Route("{shoppingCartId}/clear")]
    public async Task<IActionResult> ClearShoppingCart(Guid shoppingCartId, CancellationToken cancellation)
    {
        await _shoppingCart.ClearShoppingCart(shoppingCartId, cancellation);
        return Ok();
    }

    [HttpPost]
    [Route("{shoppingCartId}/item")]
    public async Task<IActionResult> AddCartItemToShoppingCart(Guid shoppingCartId, [FromBody]AddItemToShoppingCartDTO dto, CancellationToken cancellation)
    {
        await _shoppingCart.AddItemToShoppingCart(dto, cancellation);
        return Ok();
    }

    [HttpDelete]
    [Route("{shoppingCartId}/item/{cartItemId}")]
    public async Task<IActionResult> RemoveCartItemFromShoppingCart(Guid shoppingCartId, Guid cartItemId, CancellationToken cancellation)
    {
        await _shoppingCart.RemoveCartItemFromShoppingCart(shoppingCartId, cartItemId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{shoppingCartId}/item/{cartItemId}/increment")]
    public async Task<IActionResult> IncrementTheQuantityOfTheCartItem(Guid shoppingCartId, Guid cartItem, [FromBody]IncrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation)
    {
        if (shoppingCartId != dto.ShoppingCartId || cartItem != dto.CartItemId)
        {
            return BadRequest("Os Ids do carrinho e item do carrinho precisam ser iguais aos informados no body da requisição");
        }

        await _shoppingCart.IncrementTheQuantityOfTheCartItem(dto, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("{shoppingCartId}/item/{cartItemId}/decrement")]
    public async Task<IActionResult> IncrementTheQuantityOfTheCartItem(Guid shoppingCartId, Guid cartItem, [FromBody]DecrementTheQuantityOfTheCartItemDTO dto, CancellationToken cancellation)
    {
        if (shoppingCartId != dto.ShoppingCartId || cartItem != dto.CartItemId)
        {
            return BadRequest("Os Ids do carrinho e item do carrinho precisam ser iguais aos informados no body da requisição");
        }

        await _shoppingCart.DecrementTheQuantityOfTheCartItem(dto, cancellation);
        return Ok();
    }
}