using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Cart;
using FIAP.Diner.Application.Checkout.Confirmation;
using FIAP.Diner.Application.Checkout.Requirement;
using FIAP.Diner.Application.CustomerManagement.Identification;
using FIAP.Diner.Application.CustomerManagement.Registration;
using FIAP.Diner.Application.Menu.Management;
using FIAP.Diner.Application.Menu.Query;
using FIAP.Diner.Application.Order.ConsultOrder;
using FIAP.Diner.Application.Order.Tracking;
using FIAP.Diner.Domain.Cart;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.CustomerManagement.Customers;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.Order;
using FIAP.Diner.Infrastructure.CQRS;
using FIAP.Diner.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Diner.API.Configuration
{
    public static class DepedencyInjectionExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddDispatchers();
            services.AddApplication();

            services.MockRepository();
        }

        private static void AddDispatchers(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();
        }

        private static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<AddItemToCartCommand>, CartManagementHandler>();
            services.AddScoped<ICommandHandler<RemoveItemFromCartCommand>, CartManagementHandler>();
            services.AddScoped<ICommandHandler<UpdateCartItemProductInformation>, CartManagementHandler>();
            services.AddScoped<ICommandHandler<CloseCartCommand>, CartManagementHandler>();
            services.AddScoped<IQueryHandler<GetCartItemsQuery, CartDetails>, CartManagementHandler>();

            services.AddScoped<ICommandHandler<ConfirmPaymentCommand>, PaymentConfirmationHandler>();
            services.AddScoped<ICommandHandler<RefusePaymentCommand>, PaymentConfirmationHandler>();

            services.AddScoped<IEventHandler<CartClosedDomainEvent>, GeneratePaymentEventHandler>();
            services.AddScoped<IQueryHandler<RequirePaymentQuery, RequiredPayment>, PaymentRequirementHandler>();

            services.AddScoped<IQueryHandler<IdentifyCustomerQuery, IdentifiedCustomer>, CustomerIdentificationHandler>();

            services.AddScoped<ICommandHandler<RegisterCustomerCommand>, CustomerRegistrationHandler>();
            services.AddScoped<ICommandHandler<UpdateCustomerRegistrationInformationCommand>, CustomerRegistrationHandler>();

            services.AddScoped<ICommandHandler<RegisterProductCommand>, ProductManagementHandler>();
            services.AddScoped<ICommandHandler<UpdateProductCommand>, ProductManagementHandler>();
            services.AddScoped<ICommandHandler<RemoveProductCommand>, ProductManagementHandler>();
            services.AddScoped<IQueryHandler<GetProductsQuery, IEnumerable<Product>>, ProductManagementHandler>();

            services.AddScoped<IQueryHandler<GetProductsByCategoryQuery, IEnumerable<ProductDetails>>, ProductsByCategoryQueryHandler>();

            services.AddScoped<IQueryHandler<ConsultOrderQuery, OrderDetails>, ConsultOrderHandler>();

            services.AddScoped<ICommandHandler<UpdateOrderTrackingCommand>, OrderHandler>();

            services.AddScoped<IEventHandler<PaymentConfirmedDomainEvent>, OrderRegisterEventHandler>();

        }

        private static void MockRepository(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, MockRepository>();
            services.AddScoped<IPaymentRepository, MockRepository>();
            services.AddScoped<ICustomerRepository, MockRepository>();
            services.AddScoped<IProductRepository, MockRepository>();
            services.AddScoped<IOrderRepository, MockRepository>();
            services.AddScoped<IExternalPaymentService, MockRepository>();
        }
    }
}
