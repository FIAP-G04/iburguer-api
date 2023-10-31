using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.Customers;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.Orders;
using FIAP.Diner.Domain.ShoppingCarts;
using FIAP.Diner.Infrastructure.Data.Modules.Checkout;
using FIAP.Diner.Infrastructure.Data.Modules.Customers;
using FIAP.Diner.Infrastructure.Data.Modules.Menu;
using FIAP.Diner.Infrastructure.Data.Modules.Orders;
using FIAP.Diner.Infrastructure.Data.Modules.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using SequenceValue = System.Int32;
namespace FIAP.Diner.Infrastructure.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) =>
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> Images { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderTracking> OrderTrackings { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.EnableServiceProviderCaching(false);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("sq_order_number").IncrementsBy(1).HasMax(10000000).StartsAt(1).IsCyclic();

        modelBuilder.ApplyConfiguration(new CustomerMapping());
        modelBuilder.ApplyConfiguration(new ProductImageMapping());
        modelBuilder.ApplyConfiguration(new ProductMapping());
        modelBuilder.ApplyConfiguration(new ShoppingCartMapping());
        modelBuilder.ApplyConfiguration(new CartItemMapping());
        modelBuilder.ApplyConfiguration(new OrderMapping());
        modelBuilder.ApplyConfiguration(new OrderTrackingMapping());
        modelBuilder.ApplyConfiguration(new PaymentMapping());

        base.OnModelCreating(modelBuilder);
    }
}