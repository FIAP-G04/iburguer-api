using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Application.Menu.Management
{
    public class ProductManagementHandler : ICommandHandler<RegisterProductCommand>,
                                            ICommandHandler<UpdateProductCommand>,
                                            ICommandHandler<RemoveProductCommand>,
                                            IQueryHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;

        public ProductManagementHandler(IProductRepository repository) => _repository = repository;

        public async Task Handle(RegisterProductCommand command, CancellationToken cancellation)
        {
            var product = new Product(
                command.Name,
                command.Description,
                command.Price,
                command.Category,
                command.ReadyTimeExpectation,
                command.ImageURLs);

            await _repository.Save(product, cancellation);
        }

        public async Task Handle(UpdateProductCommand command, CancellationToken cancellation)
        {
            var product = await _repository.Get(command.ProductId, cancellation) ?? throw new ProductNotFoundException(command.ProductId);

            product.Update(
                command.Name,
                command.Description,
                command.Price,
                command.Category,
                command.ReadyTimeExpectation,
                command.ImageURLs);

            await _repository.Update(product, cancellation);

        }
        public async Task Handle(RemoveProductCommand command, CancellationToken cancellation)
        {
            var product = await _repository.Get(command.ProductId, cancellation) ?? throw new ProductNotFoundException(command.ProductId);

            await _repository.Remove(product, cancellation);
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery query, CancellationToken cancellation)
            => await _repository.Get(
                query.ProductId,
                query.Name,
                query.Description,
                query.Category,
                cancellation);
    }
}
