using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data.Modules.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly Context _context;

    public CustomerRepository(Context context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public DbSet<Customer> Set => _context.Set<Customer>();

    public async Task<Customer?> GetByCpf(CPF cpf, CancellationToken cancellationToken) =>
        await Set.FirstOrDefaultAsync(e => e.CPF == cpf, cancellationToken);

    public async Task<Customer?> GetById(CustomerId id, CancellationToken cancellationToken) =>
        await Set.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task Register(Customer customer, CancellationToken cancellationToken)
    {
        await Set.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCustomerRegistration(Customer customer, CancellationToken cancellationToken)
    {
        Set.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}