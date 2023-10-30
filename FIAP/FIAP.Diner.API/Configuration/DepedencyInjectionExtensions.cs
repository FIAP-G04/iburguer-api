using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Checkout;
using FIAP.Diner.Application.Customers.Identification;
using FIAP.Diner.Application.Customers.Registration;
using FIAP.Diner.Application.Menu;
using FIAP.Diner.Application.Orders;
using FIAP.Diner.Application.ShoppingCarts;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Infrastructure.Data.Modules.Orders;
using FIAP.Diner.Infrastructure.Dispatchers;

namespace FIAP.Diner.API.Configuration;

public static class DepedencyInjectionExtensions
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddDispatchers();
        services.AddCustomerModule();
        services.AddMenuModule();
        services.AddShoppingCartModule();
        services.AddOrderModule();
        services.AddCheckoutModule();
    }

    private static void AddCustomerModule(this IServiceCollection services)
    {
        services.AddScoped<ICustomerIdentifier, CustomerIdentificationService>();
        services.AddScoped<ICustomerAccount, CustomerAccountService>();
    }

    private static void AddMenuModule(this IServiceCollection services)
    {
        services.AddScoped<IMenuManagement, MenuService>();
    }

    private static void AddShoppingCartModule(this IServiceCollection services)
    {
        services.AddScoped<IShoppingCart, ShoppingCartService>();
    }

    private static void AddOrderModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRetriever, OrderRetriever>();
    }

    private static void AddCheckoutModule(this IServiceCollection services)
    {
        services.AddScoped<ICheckoutService, CheckoutService>();
    }

    private static void AddDispatchers(this IServiceCollection services)
    {
        services.AddScoped<IEventDispatcher, EventDispatcher>();
    }
}