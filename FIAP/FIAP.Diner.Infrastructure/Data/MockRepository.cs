using FIAP.Diner.Domain.Cart;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.Customers;
using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.Order;
using CartId = FIAP.Diner.Domain.Cart.CartId;

namespace FIAP.Diner.Infrastructure.Data;

public class MockRepository : ICartRepository, IPaymentRepository,
    IOrderRepository, IExternalPaymentService
{
    public Task<Cart> Get(CustomerId2 customerId2, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<IEnumerable<Cart>> GetByProductInCart(ProductId productId,
        CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<CustomerId2> GetCustomerId(CartId cartId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<CartDetails>
        GetDetailed(CustomerId2 customerId2, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<IEnumerable<Cart>> GetByProductInCart(Domain.Cart.ProductId productId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task Save(Cart cart, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task Update(Cart cart, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<(string, string)> GenerateQRCode(decimal amount) =>
        throw new NotImplementedException();

    public Task<Order> Get(Guid id, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task<OrderDetails> GetDetails(Guid customerId, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task<string> GetNextWithdrawCode(CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task<IEnumerable<OrderDetails>> GetQueue(CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task Save(Order order, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task Update(Order order, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task<Payment> Get(string externalId, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task<Payment> Get(Domain.Checkout.CartId cartId, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task Save(Payment payment, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task Update(Payment payment, CancellationToken cancellation) =>
        throw new NotImplementedException();




    public Task Remove(Product product, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task Save(Product product, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task Update(Product product, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<Customer> GetByCpf(CPF cpf, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task Register(Customer customer, CancellationToken cancellation) =>
        throw new NotImplementedException();

    public Task UpdateCustomerRegistration(Customer customer, CancellationToken cancellation) =>
        throw new NotImplementedException();
}