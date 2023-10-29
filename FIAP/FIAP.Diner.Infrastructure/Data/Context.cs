using FIAP.Diner.Domain.Customers;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Infrastructure.Data.Modules.Customers;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) =>
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Product>? Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.EnableServiceProviderCaching(false);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerMapping());

        base.OnModelCreating(modelBuilder);
    }
}