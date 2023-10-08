using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Checkout
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task Save(Payment payment);

        Task Update(Payment payment);

        Task<Payment> Get(string externalId);
    }
}
