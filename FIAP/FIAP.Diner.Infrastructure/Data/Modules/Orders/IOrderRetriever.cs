using FIAP.Diner.Application.Common;
using FIAP.Diner.Application.Orders;

namespace FIAP.Diner.Infrastructure.Data.Modules.Orders;

public interface IOrderRetriever
{
    Task<PaginatedList<OrderSummaryDTO>> GetPagedOrdersAsync(int page, int limit,
        CancellationToken cancellation);


}