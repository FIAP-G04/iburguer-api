using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Menu;

public record ProductUpdatedDomainEvent(ProductId ProductId, Price newPrice, Price oldPrice) : IDomainEvent;