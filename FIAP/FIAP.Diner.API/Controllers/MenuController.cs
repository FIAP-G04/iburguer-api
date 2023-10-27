using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Menu.Management;
using FIAP.Diner.Application.Menu.Query;
using FIAP.Diner.Domain.Menu;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public MenuController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("product")]
        public async Task<IActionResult> GetProducts([FromQuery] Guid? productId, [FromQuery] string? name, [FromQuery] string? description, [FromQuery] Category? category)
        {
            var query = new GetProductsQuery(productId, name, description, category);
            var result = await _queryDispatcher.Dispatch<GetProductsQuery, IEnumerable<ProductDetails>>(query, default);
            return Ok(result);
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> RegisterProduct(RegisterProductCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }

        [HttpPut]
        [Route("product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }

        [HttpDelete]
        [Route("product")]
        public async Task<IActionResult> RemoveProduct(RemoveProductCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }

        [HttpGet]
        [Route("product/{category}")]
        public async Task<IActionResult> GetProductsByCategory(Category category)
        {
            var query = new GetProductsByCategoryQuery(category);
            var result = await _queryDispatcher.Dispatch<GetProductsByCategoryQuery, IEnumerable<ProductDetails>>(query, default);
            return Ok(result);
        }
    }
}
