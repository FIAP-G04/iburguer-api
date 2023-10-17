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
        var (externalPaymentId, qrCodeValue) = await _externalPaymentService.GenerateQRCode(query.Amount);

        if (string.IsNullOrEmpty(externalPaymentId) || string.IsNullOrEmpty(qrCodeValue))
            throw new PaymentGenerationException(query.OrderId);

        var qrCode = new QRCode(externalPaymentId, qrCodeValue);

        var payment = new Payment(query.OrderId, query.Amount, qrCode);

        await _paymentRepository.Save(payment);

        return new(payment);
    }
}