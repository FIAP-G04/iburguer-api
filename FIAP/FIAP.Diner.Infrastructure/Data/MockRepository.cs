using FIAP.Diner.Domain.Cart;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.CustomerManagement.Customers;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Infrastructure.Data
{
    public class MockRepository : ICartRepository, IPaymentRepository, ICustomerRepository, IProductRepository, IOrderRepository, IExternalPaymentService
    {
        public Task<(string, string)> GenerateQRCode(decimal amount) => throw new NotImplementedException();
        public Task<Cart> Get(Domain.Cart.CustomerId customerId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Payment> Get(string externalId, CancellationToken cancellation) => throw new NotImplementedException();
        public Task<Payment> Get(Domain.Checkout.CartId cartId, CancellationToken cancellation) => throw new NotImplementedException();
        public Task<Product> Get(Domain.Menu.ProductId id, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<IEnumerable<Product>> Get(Domain.Menu.ProductId? id, string? name, string? description, Category? category, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Order> Get(Guid id, CancellationToken cancellation) => throw new NotImplementedException();
        public Task<IEnumerable<ProductDetails>> GetByCategory(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Customer> GetByCpf(CPF cpf, CancellationToken cancellation) => throw new NotImplementedException();
        public Task<Customer> GetById(Domain.CustomerManagement.Customers.CustomerId id, CancellationToken cancellation) => throw new NotImplementedException();
        public Task<IEnumerable<Cart>> GetByProductInCart(Domain.Cart.ProductId productId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Domain.Cart.CustomerId> GetCustomerId(Domain.Cart.CartId cartId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<CartDetails> GetDetailed(Domain.Cart.CustomerId customerId, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<OrderDetails> GetDetails(Guid customerId, CancellationToken cancellation) => throw new NotImplementedException();
        public Task<string> GetNextWithdrawCode(CancellationToken cancellation) => throw new NotImplementedException();
        public Task<IEnumerable<OrderDetails>> GetQueue(CancellationToken cancellation) => throw new NotImplementedException();
        public Task Register(Customer customer, CancellationToken cancellation) => throw new NotImplementedException();
        public Task Remove(Product product, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task Save(Cart cart, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task Save(Payment payment, CancellationToken cancellation) => throw new NotImplementedException();
        public Task Save(Product product, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task Save(Order order, CancellationToken cancellation) => throw new NotImplementedException();
        public Task Update(Cart cart, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task Update(Payment payment, CancellationToken cancellation) => throw new NotImplementedException();
        public Task Update(Product product, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task Update(Order order, CancellationToken cancellation) => throw new NotImplementedException();
        public Task UpdateCustomerRegistration(Customer customer, CancellationToken cancellation) => throw new NotImplementedException();
    }
}
