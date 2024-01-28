using FIAP.Diner.Application.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/carts")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly ICreateAnonymousShoppingCartUseCase _createAnonymousShoppingCartUseCase;
    private readonly ICreateCustomerShoppingCartUseCase _createCustomerShoppingCartUseCase;
    private readonly IAddItemToShoppingCartUseCase _addItemToShoppingCartUseCase;
    private readonly IClearShoppingCartUseCase _clearShoppingCartUseCase;
    private readonly IRemoveCartItemFromShoppingCartUseCase _removeCartItemFromShoppingCartUseCase;
    private readonly IIncrementTheQuantityOfTheCartItemUseCase _incrementTheQuantityOfTheCartItemUseCase;
    private readonly IDecrementTheQuantityOfTheCartItemUseCase _decrementTheQuantityOfTheCartItemUseCase;

    public ShoppingCartController(ICreateAnonymousShoppingCartUseCase createAnonymousShoppingCartUseCase, ICreateCustomerShoppingCartUseCase createCustomerShoppingCartUseCase, IAddItemToShoppingCartUseCase addItemToShoppingCartUseCase, IClearShoppingCartUseCase clearShoppingCartUseCase, IRemoveCartItemFromShoppingCartUseCase removeCartItemFromShoppingCartUseCase, IIncrementTheQuantityOfTheCartItemUseCase incrementTheQuantityOfTheCartItemUseCase, IDecrementTheQuantityOfTheCartItemUseCase decrementTheQuantityOfTheCartItemUseCase)
    {
        _createAnonymousShoppingCartUseCase = createAnonymousShoppingCartUseCase;
        _createCustomerShoppingCartUseCase = createCustomerShoppingCartUseCase;
        _addItemToShoppingCartUseCase = addItemToShoppingCartUseCase;
        _clearShoppingCartUseCase = clearShoppingCartUseCase;
        _removeCartItemFromShoppingCartUseCase = removeCartItemFromShoppingCartUseCase;
        _incrementTheQuantityOfTheCartItemUseCase = incrementTheQuantityOfTheCartItemUseCase;
        _decrementTheQuantityOfTheCartItemUseCase = decrementTheQuantityOfTheCartItemUseCase;

    }

    [HttpPost]
    public async Task<IActionResult> CreateShoppingCart(CreateShoppingCartDTO dto, CancellationToken cancellation)
    {
        if (dto.CustomerId == null)
        {
            var shoppingCartId = await _createAnonymousShoppingCartUseCase.CreateAnonymousShoppingCart(cancellation);

            return Ok(shoppingCartId);
        }
        else
        {
            var shoppingCartId = await _createCustomerShoppingCartUseCase.CreateCustomerShoppingCart(dto.CustomerId.Value, cancellation);

            return Ok(shoppingCartId);
        }
    }

    [HttpPatch]
    [Route("{shoppingCartId}/clear")]
    public async Task<IActionResult> ClearShoppingCart(Guid shoppingCartId, CancellationToken cancellation)
    {
        await _clearShoppingCartUseCase.ClearShoppingCart(shoppingCartId, cancellation);
        return Ok();
    }

    [HttpPost]
    [Route("{shoppingCartId}/item")]
    public async Task<IActionResult> AddCartItemToShoppingCart(Guid shoppingCartId, [FromBody]AddItemToShoppingCartDTO dto, CancellationToken cancellation)
    {
        await _addItemToShoppingCartUseCase.AddItemToShoppingCart(dto, cancellation);
        return Ok();
    }

    [HttpDelete]
    [Route("{shoppingCartId}/item/{cartItemId}")]
    public async Task<IActionResult> RemoveCartItemFromShoppingCart(Guid shoppingCartId, Guid cartItemId, CancellationToken cancellation)
    {
        await _removeCartItemFromShoppingCartUseCase.RemoveCartItemFromShoppingCart(shoppingCartId, cartItemId, cancellation);
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

        await _incrementTheQuantityOfTheCartItemUseCase.IncrementTheQuantityOfTheCartItem(dto, cancellation);
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

        await _decrementTheQuantityOfTheCartItemUseCase.DecrementTheQuantityOfTheCartItem(dto, cancellation);
        return Ok();
    }
}