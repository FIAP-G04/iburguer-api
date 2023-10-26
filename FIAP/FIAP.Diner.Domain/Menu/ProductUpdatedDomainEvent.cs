using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu
{
    public record ProductUpdatedDomainEvent(ProductId ProductId, decimal Price) : IDomainEvent;
}
