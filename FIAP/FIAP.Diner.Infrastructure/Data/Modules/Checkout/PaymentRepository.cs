using FIAP.Diner.Domain.Checkout;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data.Modules.Checkout;

public class PaymentRepository : IPaymentRepository
{
    private readonly Context _context;

    public PaymentRepository(Context context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public DbSet<Payment> Set => _context.Set<Payment>();

    public async Task Save(Payment payment, CancellationToken cancellation)
    {
        await Set.AddAsync(payment, cancellation);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task Update(Payment payment, CancellationToken cancellation)
    {
        Set.Update(payment);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<Payment?> GetById(ProductId paymentId, CancellationToken cancellation)
    {
        return await Set.FirstOrDefaultAsync(p => p.Id == paymentId, cancellation);
    }
}