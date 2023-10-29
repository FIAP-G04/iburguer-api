using FIAP.Diner.Domain.Menu;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data.Modules.Menu;

public class ProductRepository : IProductRepository
{
    private readonly Context _context;

    public ProductRepository(Context context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public DbSet<Product> Set => _context.Set<Product>();

    public async Task Save(Product product, CancellationToken cancellationToken)
    {
        await Set.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(Product product, CancellationToken cancellationToken)
    {
        Set.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(Product product, CancellationToken cancellationToken)
    {
        var entity = await GetById(product.Id, cancellationToken);

        if (entity != null)
        {
            Set.Remove(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetById(ProductId id, CancellationToken cancellationToken)
    {
        return await Set.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByCategory(Category category,
        CancellationToken cancellationToken)
    {
        return await Set.Where(p => p.Enabled == true && p.Category == category).ToListAsync(cancellationToken);
    }
}