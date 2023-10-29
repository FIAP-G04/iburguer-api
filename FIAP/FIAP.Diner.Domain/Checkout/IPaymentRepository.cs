using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Checkout;

public interface IPaymentRepository : IRepository<Payment>
{
    Task Save(Payment payment, CancellationToken cancellation);

    Task Update(Payment payment, CancellationToken cancellation);

    Task<Payment> Get(string externalId, CancellationToken cancellation);

    Task<Payment> Get(CartId cartId, CancellationToken cancellation);
}