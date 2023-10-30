using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Orders;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Diner.Infrastructure.Data.Configurations;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddScoped<IEventHandler<PaymentRequestedDomainEvent>, OrderEventHandler>();
        services.AddScoped<IEventHandler<PaymentConfirmedDomainEvent>, OrderEventHandler>();
        return services;
    }

    private static void AddDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Connection"),
                assembly => assembly.MigrationsAssembly("FIAP.Diner.Infrastructure"));
        });

        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
    }



    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }

}