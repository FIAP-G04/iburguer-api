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
        services.AddScoped<IIdentifyCustomerUseCase, IdentifyCustomerUseCase>();
        services.AddScoped<IRegisterCustomerUseCase, RegisterCustomerUseCase>();
        services.AddScoped<IUpdateCustomerRegistrationInformationUseCase, UpdateCustomerRegistrationInformationUseCase>();
    }

    private static void AddMenuModule(this IServiceCollection services)
    {
        services.AddScoped<IAddProductToMenuUseCase, AddProductToMenuUseCase>();
        services.AddScoped<IRemoveProductFromMenuUseCase, RemoveProductFromMenuUseCase>();
        services.AddScoped<IChangeMenuProductUseCase, ChangeMenuProductUseCase>();
        services.AddScoped<IDisableMenuProductUseCase, DisableMenuProductUseCase>();
        services.AddScoped<IEnableMenuProductUseCase, EnableMenuProductUseCase>();
        services.AddScoped<IGetByCategoryUseCase, GetByCategoryUseCase>();
    }

    private static void AddShoppingCartModule(this IServiceCollection services)
    {
        services.AddScoped<IShoppingCart, ShoppingCartService>();
    }

    private static void AddOrderModule(this IServiceCollection services)
    {
        services.AddScoped<ICancelOrderUseCase, CancelOrderUseCase>();
        services.AddScoped<ICompleteOrderUseCase, CompleteOrderUseCase>();
        services.AddScoped<IConfirmOrderUseCase, ConfirmOrderUseCase>();
        services.AddScoped<IDeliverOrderUseCase, DeliverOrderUseCase>();
        services.AddScoped<IRegisterOrderUseCase, RegisterOrderUseCase>();
        services.AddScoped<IStartOrderUseCase, StartOrderUseCase>();
        services.AddScoped<IOrderRetriever, OrderRetriever>();

        services.AddScoped<IEventHandler<PaymentRequestedDomainEvent>, OrderEventHandler>();
        services.AddScoped<IEventHandler<PaymentConfirmedDomainEvent>, OrderEventHandler>();
    }

    private static void AddCheckoutModule(this IServiceCollection services)
    {
        services.AddScoped<ICheckoutUseCase, ICheckoutUseCase>();
        services.AddScoped<IGetPaymentStatusUseCase, GetPaymentStatusUseCase>();
        services.AddScoped<IRefusePaymentUseCase, RefusePaymentUseCase>();
        services.AddScoped<IConfirmPaymentUseCase, ConfirmPaymentUseCase>();
    }

    private static void AddDispatchers(this IServiceCollection services)
    {
        services.AddScoped<IEventDispatcher, EventDispatcher>();
    }
}