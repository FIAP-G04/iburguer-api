using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu.Management
{
    public record GetProductsQuery(Guid? ProductId, string? Name, string? Description, Category? Category);
}
