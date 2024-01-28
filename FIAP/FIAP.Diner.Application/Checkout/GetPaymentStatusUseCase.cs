using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout;

public interface IGetPaymentStatusUseCase
{
    Task<PaymentStatusDTO> GetPaymentStatus(Guid paymentId, CancellationToken cancellation);
}

public class GetPaymentStatusUseCase : IGetPaymentStatusUseCase
{
    private readonly IPaymentRepository _repository;

    public GetPaymentStatusUseCase(IPaymentRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IPaymentRepository));

        _repository = repository;
    }

    public async Task<PaymentStatusDTO> GetPaymentStatus(Guid paymentId, CancellationToken cancellation)
    {
        var payment = await _repository.GetById(paymentId, cancellation);

        if (payment is null)
            throw new PaymentNotFoundException(paymentId);

        return new(payment.Id, payment.Status);
    }
}