using FIAP.Diner.Domain.Customers;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Infrastructure.Data.Modules.Customers;
using FIAP.Diner.Infrastructure.Data.Modules.Menu;
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

        return services;
    }

    private static void AddDbContext(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Connection"),
                assembly => assembly.MigrationsAssembly("FIAP.Diner.Infrastructure"));
        });

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

}