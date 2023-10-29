using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Cart;
using FIAP.Diner.Application.Checkout.Confirmation;
using FIAP.Diner.Application.Checkout.Requirement;
using FIAP.Diner.Application.Customers.Identification;
using FIAP.Diner.Application.Customers.Registration;
using FIAP.Diner.Application.Menu;
using FIAP.Diner.Application.Order.ConsultOrder;
using FIAP.Diner.Application.Order.Tracking;
using FIAP.Diner.Domain.Cart;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.Order;
using FIAP.Diner.Infrastructure.CQRS;
using FIAP.Diner.Infrastructure.Data;

namespace FIAP.Diner.API.Configuration;

public static class DepedencyInjectionExtensions
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddDispatchers();
        services.AddApplication();

        services.MockRepository();

        services.AddCustomerModule();
        services.AddMenuModule();
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

    private static void AddDispatchers(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped<IEventDispatcher, EventDispatcher>();
    }

    private static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IHandler<AddItemToCartCommand>, CartManagementHandler>();
        services.AddScoped<IHandler<RemoveItemFromCartCommand>, CartManagementHandler>();
        services
            .AddScoped<IHandler<UpdateCartItemProductInformation>, CartManagementHandler>();
        services.AddScoped<IHandler<CloseCartCommand>, CartManagementHandler>();
        services.AddScoped<IQueryHandler<GetCartItemsQuery, CartDetails>, CartManagementHandler>();

        services.AddScoped<IHandler<ConfirmPaymentCommand>, PaymentConfirmationHandler>();
        services.AddScoped<IHandler<RefusePaymentCommand>, PaymentConfirmationHandler>();

        services.AddScoped<IEventHandler<CartClosedDomainEvent>, GeneratePaymentEventHandler>();
        services
            .AddScoped<IQueryHandler<RequirePaymentQuery, RequiredPayment>,
                PaymentRequirementHandler>();





        services.AddScoped<IQueryHandler<ConsultOrderQuery, OrderDetails>, ConsultOrderHandler>();

        services.AddScoped<IHandler<UpdateOrderTrackingCommand>, OrderHandler>();

        services.AddScoped<IEventHandler<PaymentConfirmedDomainEvent>, OrderRegisterEventHandler>();
    }

    private static void MockRepository(this IServiceCollection services)
    {
        services.AddScoped<ICartRepository, MockRepository>();
        services.AddScoped<IPaymentRepository, MockRepository>();
        services.AddScoped<IOrderRepository, MockRepository>();
        services.AddScoped<IExternalPaymentService, MockRepository>();
    }
}