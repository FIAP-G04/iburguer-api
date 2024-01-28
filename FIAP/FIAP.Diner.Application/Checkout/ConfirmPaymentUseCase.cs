using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout;

public interface IConfirmPaymentUseCase
{
    Task ConfirmPayment(Guid paymentId, CancellationToken cancellation);
}

public class ConfirmPaymentUseCase : IConfirmPaymentUseCase
{
    private readonly IPaymentRepository _repository;

    public ConfirmPaymentUseCase(IPaymentRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IPaymentRepository));

        _repository = repository;
    }

    public async Task ConfirmPayment(Guid paymentId, CancellationToken cancellation)
    {
        var payment = await _repository.GetById(paymentId, cancellation);

        if (payment is null)
            throw new PaymentNotFoundException(paymentId);

        payment.Confirm();

        await _repository.Update(payment, cancellation);
    }
}