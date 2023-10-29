using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout.Confirmation;

public class PaymentConfirmationHandler : IHandler<ConfirmPaymentCommand>,
    IHandler<RefusePaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentConfirmationHandler(IPaymentRepository paymentRepository) =>
        _paymentRepository = paymentRepository;

    public async Task Handle(ConfirmPaymentCommand command, CancellationToken cancellation)
    {
        var payment = await _paymentRepository.Get(command.ExternalPaymentServiceId, cancellation);

        if (payment is null)
            throw new PaymentNotExistsException(command.ExternalPaymentServiceId);

        payment.Confirm(command.PayedAt);

        await _paymentRepository.Update(payment, cancellation);
    }

    public async Task Handle(RefusePaymentCommand command, CancellationToken cancellation)
    {
        var payment = await _paymentRepository.Get(command.ExternalPaymentServiceId, cancellation);

        if (payment is null)
            throw new PaymentNotExistsException(command.ExternalPaymentServiceId);

        payment.Refuse();

        await _paymentRepository.Update(payment, cancellation);
    }
}