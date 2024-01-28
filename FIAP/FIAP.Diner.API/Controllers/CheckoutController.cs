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
    private readonly ICheckoutUseCase _checkoutUseCase;

    public CheckoutController(ICheckoutService checkoutService, ICheckoutUseCase checkoutUseCase, IEventHandler<PaymentRequestedDomainEvent> handler)
    {
        _checkoutService = checkoutService;
        _checkoutUseCase = checkoutUseCase;
    }

    [HttpPost]
    [Route("cart/{shoppingCartId}")]
    public async Task<IActionResult> Checkout(Guid shoppingCartId, CancellationToken cancellation)
    {
        return Ok(await _checkoutUseCase.Checkout(shoppingCartId, cancellation));
    }

    [HttpGet]
    [Route("{paymentId}/status")]
    public async Task<IActionResult> GetStatus(Guid paymentId, CancellationToken cancellation)
        => Ok(await _checkoutService.GetPaymentStatus(paymentId, cancellation));

    [HttpPut]
    [Route("{paymentId}/confirm")]
    public async Task<IActionResult> Confirm(Guid paymentId, CancellationToken cancellation)
    {
        await _checkoutService.ConfirmPayment(paymentId, cancellation);
        return Ok();
    }

    [HttpPut]
    [Route("{paymentId}/refuse")]
    public async Task<IActionResult> Refuse(Guid paymentId, CancellationToken cancellation)
    {
        await _checkoutService.RefusePayment(paymentId, cancellation);
        return Ok();
    }
}