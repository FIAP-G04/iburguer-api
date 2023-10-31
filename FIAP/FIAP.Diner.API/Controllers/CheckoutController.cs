using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Checkout;
using FIAP.Diner.Domain.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/checkout")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutService _checkoutService;

    public CheckoutController(ICheckoutService checkoutService, IEventHandler<PaymentRequestedDomainEvent> handler)
    {
        _checkoutService = checkoutService;
    }

    [HttpPost]
    [Route("cart/{shoppingCartId}")]
    public async Task<IActionResult> Checkout(Guid shoppingCartId, CancellationToken cancellation)
    {
        await _checkoutService.Checkout(shoppingCartId, cancellation);
        return Ok();
    }
}