using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout;

public interface IRefusePaymentUseCase
{
    Task RefusePayment(Guid paymentId, CancellationToken cancellation);
}

public class RefusePaymentUseCase : IRefusePaymentUseCase
{
    private readonly IPaymentRepository _repository;

    public RefusePaymentUseCase(IPaymentRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IPaymentRepository));

        _repository = repository;
    }

    public async Task RefusePayment(Guid paymentId, CancellationToken cancellation)
    {
        var payment = await _repository.GetById(paymentId, cancellation);

        if (payment is null)
            throw new PaymentNotFoundException(paymentId);

        payment.Refuse();

        await _repository.Update(payment, cancellation);
    }
}