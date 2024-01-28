using FIAP.Diner.Application.Menu;
using FIAP.Diner.Domain.Menu;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/menu")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IAddProductToMenuUseCase _addProductToMenuUseCase;
    private readonly IRemoveProductFromMenuUseCase _removeProductFromMenuUseCase;
    private readonly IChangeMenuProductUseCase _changeMenuProductUseCase;
    private readonly IDisableMenuProductUseCase _disableMenuProductUseCase;
    private readonly IEnableMenuProductUseCase _enableMenuProductUseCase;
    private readonly IGetByCategoryUseCase _getByCategoryUseCase;

    public MenuController(
        IAddProductToMenuUseCase addProductToMenuUseCase,
        IRemoveProductFromMenuUseCase removeProductFromMenuUseCase,
        IChangeMenuProductUseCase changeMenuProductUseCase,
        IDisableMenuProductUseCase disableMenuProductUseCase,
        IEnableMenuProductUseCase enableMenuProductUseCase,
        IGetByCategoryUseCase getByCategoryUseCase)
    {
        _addProductToMenuUseCase = addProductToMenuUseCase;
        _removeProductFromMenuUseCase = removeProductFromMenuUseCase;
        _changeMenuProductUseCase = changeMenuProductUseCase;
        _disableMenuProductUseCase = disableMenuProductUseCase;
        _enableMenuProductUseCase = enableMenuProductUseCase;
        _getByCategoryUseCase = getByCategoryUseCase;
    }

    [HttpPost]
    [Route("products")]
    public async Task<IActionResult> AddProductToMenu(ProductDTO dto, CancellationToken cancellation)
    {
        await _addProductToMenuUseCase.AddProductToMenu(dto, cancellation);
        return Ok();
    }

    [HttpPut]
    [Route("products")]
    public async Task<IActionResult> ChangeMenuProduct(ProductDTO dto, CancellationToken cancellation)
    {
        await _changeMenuProductUseCase.ChangeMenuProduct(dto, cancellation);
        return Ok();
    }

    [HttpDelete]
    [Route("products")]
    public async Task<IActionResult> RemoveProduct(Guid productId, CancellationToken cancellation)
    {
        await _removeProductFromMenuUseCase.RemoveProductFromMenu(productId, cancellation);
        return NoContent();
    }

    [HttpPatch]
    [Route("products/enabled")]
    public async Task<IActionResult> EnableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        await _enableMenuProductUseCase.EnableMenuProduct(productId, cancellation);
        return Ok();
    }

    [HttpPatch]
    [Route("products/disabled")]
    public async Task<IActionResult> DisableMenuProduct(Guid productId, CancellationToken cancellation)
    {
        await _disableMenuProductUseCase.DisableMenuProduct(productId, cancellation);
        return Ok();
    }

    [HttpGet]
    [Route("products/{category}")]
    public async Task<IActionResult> GetProductsByCategory(Category category, CancellationToken cancellation)
    {
        var products = await _getByCategoryUseCase.GetByCategory(category, cancellation);
        return Ok(products);
    }
}