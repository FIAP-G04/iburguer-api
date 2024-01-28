using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Checkout;
using FIAP.Diner.Domain.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/checkout")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutUseCase _checkoutUseCase;
    private readonly IGetPaymentStatusUseCase _getPaymentStatusUseCase;
    private readonly IRefusePaymentUseCase _refusePaymentUseCase;
    private readonly IConfirmPaymentUseCase _confirmPaymentUseCase;

    public CheckoutController(
        ICheckoutUseCase checkoutUseCase,
        IGetPaymentStatusUseCase getPaymentStatusUseCase,
        IRefusePaymentUseCase refusePaymentUseCase,
        IConfirmPaymentUseCase confirmPaymentUseCase)
    {
        _checkoutUseCase = checkoutUseCase;
        _getPaymentStatusUseCase = getPaymentStatusUseCase;
        _refusePaymentUseCase = refusePaymentUseCase;
        _confirmPaymentUseCase = confirmPaymentUseCase;

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
        => Ok(await _getPaymentStatusUseCase.GetPaymentStatus(paymentId, cancellation));

    [HttpPut]
    [Route("{paymentId}/confirm")]
    public async Task<IActionResult> Confirm(Guid paymentId, CancellationToken cancellation)
    {
        await _confirmPaymentUseCase.ConfirmPayment(paymentId, cancellation);
        return Ok();
    }

    [HttpPut]
    [Route("{paymentId}/refuse")]
    public async Task<IActionResult> Refuse(Guid paymentId, CancellationToken cancellation)
    {
        await _refusePaymentUseCase.RefusePayment(paymentId, cancellation);
        return Ok();
    }
}