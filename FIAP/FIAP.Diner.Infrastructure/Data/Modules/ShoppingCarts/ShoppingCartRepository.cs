using FIAP.Diner.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data.Modules.ShoppingCarts;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly Context _context;

    public ShoppingCartRepository(Context context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public DbSet<ShoppingCart> Set => _context.Set<ShoppingCart>();

    public async Task Save(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        await Set.AddAsync(shoppingCart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        Set.Update(shoppingCart);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ShoppingCart?> GetById(ProductId id, CancellationToken cancellationToken)
    {
        return await Set.Include(p => p.Items)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}