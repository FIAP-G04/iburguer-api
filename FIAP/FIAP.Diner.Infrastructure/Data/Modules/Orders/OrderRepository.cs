using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data.Modules.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly Context _context;

    public OrderRepository(Context context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public DbSet<Order> Set => _context.Set<Order>();

    public async Task Save(Order order, CancellationToken cancellation)
    {
        await Set.AddAsync(order, cancellation);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task Update(Order order, CancellationToken cancellation)
    {
        Set.Update(order);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<Order?> GetById(ProductId orderId, CancellationToken cancellation)
    {
        return await Set.FirstOrDefaultAsync(o => o.Id == orderId, cancellation);
    }

    public async Task<int> GenerateOrderNumber()
    {
        return await _context.Database.ExecuteSqlRawAsync("SELECT currval('sq_order_number')");
    }

    /* public async Task<OrderDetails> GetOrders(ProductId orderId, CancellationToken cancellation)
    {

    }*/
}