using FIAP.Diner.Application.Abstractions;

namespace FIAP.Diner.Infrastructure.CQRS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider provider)
        {
            _serviceProvider = provider;

        }
        public async Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        {
            var instance = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TQueryResult>)) as IQueryHandler<TQuery, TQueryResult>;

            if (instance == null)
            {
                throw new InvalidOperationException("Não foi possível encontrar nenhum QueryHandler para tratar esta query.");
            }

            return await instance.Handle(query, cancellation);
        }
    }
}
