using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu.Management
{
    public record UpdateProductCommand(Guid ProductId, string Name, string Description, decimal Price, Category Category, TimeSpan ReadyTimeExpectation, IEnumerable<string> ImageURLs);
}
