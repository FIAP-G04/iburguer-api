using FIAP.Diner.Domain.Checkout;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data.Modules.Checkout;

public class PaymentRepository : IPaymentRepository
{
    private readonly IUnitOfWork<Payment> _unitOfWork;

    public PaymentRepository(IUnitOfWork<Payment> unitOfWork) =>
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task Save(Payment payment, CancellationToken cancellation)
    {
        await _unitOfWork.SaveAsync(payment, cancellation);
    }

    public async Task Update(Payment payment, CancellationToken cancellation)
    {
        await _unitOfWork.UpdateAsync(payment, cancellation);
    }

    public async Task<Payment?> GetById(ProductId paymentId, CancellationToken cancellation)
    {
        return await _unitOfWork.Set().FirstOrDefaultAsync(p => p.Id == paymentId, cancellation);
    }

    public async Task<bool> ExistsPaymentForShoppingCart(ShoppingCartId shoppingCartId, CancellationToken cancellation)
        => await _unitOfWork.Set().AnyAsync(p => p.ShoppingCart == shoppingCartId, cancellationToken: cancellation);
}