using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Menu;
using FIAP.Diner.Domain.Menu;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/menu")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMenuManagement _menu;

    public MenuController(IMenuManagement menu)
    {
        _menu = menu;
    }

    [HttpPost]
    [Route("products")]
    public async Task<IActionResult> AddProductToMenu(ProductDTO dto, CancellationToken cancellation)
    {
        await _menu.AddProductToMenu(dto, cancellation);
        return Ok();
    }

    [HttpPut]
    [Route("products")]
    public async Task<IActionResult> ChangeMenuProduct(ProductDTO dto, CancellationToken cancellation)
    {
        await _menu.ChangeMenuProduct(dto, cancellation);
        return Ok();
    }

    [HttpDelete]
    [Route("products")]
    public async Task<IActionResult> RemoveProduct(Guid productId, CancellationToken cancellation)
    {
        await _menu.RemoveProductFromMenu(productId, cancellation);
        return NoContent();
    }

    [HttpPatch]
    [Route("products/enabled")]
    public async Task<IActionResult> EnableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        await _menu.EnableMenuProduct(productId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("products/disabled")]
    public async Task<IActionResult> DisableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        await _menu.DisableMenuProduct(productId, cancellation);
        return Ok();
    }

    [HttpGet]
    [Route("products/{category}")]
    public async Task<IActionResult> GetProductsByCategory(Category category, CancellationToken cancellation)
    {
        var products = await _menu.GetByCategory(category, cancellation);
        return Ok(products);
    }
}