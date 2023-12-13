using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Checkout;

public interface IPaymentRepository : IRepository<Payment>
{
    Task Save(Payment payment, CancellationToken cancellation);

    Task Update(Payment payment, CancellationToken cancellation);

    Task<Payment?> GetById(PaymentId paymentId, CancellationToken cancellation);

    Task<bool> ExistsPaymentForShoppingCart(ShoppingCartId shoppingCartId, CancellationToken cancellation);
}