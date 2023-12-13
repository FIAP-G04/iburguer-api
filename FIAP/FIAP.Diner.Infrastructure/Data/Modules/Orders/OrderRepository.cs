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
        return await Set.Include(o => o.Trackings).FirstOrDefaultAsync(o => o.Id == orderId, cancellation);
    }

    public async Task<int> GenerateOrderNumber(CancellationToken cancellation)
    {
        return await _context.Database.SqlQuery<int>($"SELECT NEXTVAL('sq_order_number') AS \"Value\"").FirstOrDefaultAsync();
    }

    public async Task<Order?> GetOrderByShoppingCartId(Guid shoppingCartId, CancellationToken cancellation)
    {
        return await Set.Include(o => o.Trackings).FirstOrDefaultAsync(o => o.ShoppingCart == shoppingCartId, cancellation);
    }
}