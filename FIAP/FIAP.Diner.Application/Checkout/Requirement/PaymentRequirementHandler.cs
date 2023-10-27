using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout.Requirement;

public class PaymentRequirementHandler : IQueryHandler<RequirePaymentQuery, RequiredPayment>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IExternalPaymentService _externalPaymentService;

    public PaymentRequirementHandler(IPaymentRepository paymentRepository, IExternalPaymentService externalPaymentService)
    {
        _paymentRepository = paymentRepository;
        _externalPaymentService = externalPaymentService;
    }

    public async Task<RequiredPayment> Handle(RequirePaymentQuery query, CancellationToken cancellation)
    {
        var payment = await _paymentRepository.Get(query.CartId, cancellation);

        var (externalPaymentId, qrCodeValue) = await _externalPaymentService.GenerateQRCode(payment.Amount);

        if (string.IsNullOrEmpty(externalPaymentId) || string.IsNullOrEmpty(qrCodeValue))
            throw new PaymentGenerationException(query.CartId);

        var qrCode = new QRCode(externalPaymentId, qrCodeValue);

        payment.AddQRCode(qrCode);

        await _paymentRepository.Update(payment, cancellation);

        return new(payment);
    }
}