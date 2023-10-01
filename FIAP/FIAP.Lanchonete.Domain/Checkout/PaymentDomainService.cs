using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Checkout
{
    public class PaymentDomainService : IPaymentDomainService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IExternalPaymentService _externalPaymentService;

        public PaymentDomainService(IPaymentRepository paymentRepository, IExternalPaymentService mercadoPagoExternalService)
        {
            _paymentRepository = paymentRepository;
            _externalPaymentService = mercadoPagoExternalService;
        }

        public async Task<Payment> RequirePayment(Guid orderId, double amount)
        {
            var (externalPaymentId, qrCodeValue) = await _externalPaymentService.GenerateQRCode(amount);

            if (string.IsNullOrEmpty(externalPaymentId) || string.IsNullOrEmpty(qrCodeValue))
                throw new DomainException(CheckoutExceptions.ErrorGeneratingPayment);

            var qrCode = new QRCode(externalPaymentId, qrCodeValue);

            var payment = new Payment(orderId, amount, qrCode);

            await _paymentRepository.Save(payment);

            return payment;
        }

        public async Task ConfirmPayment(string externalPaymentServiceId, DateTime payedAt)
        {
            var payment = await _paymentRepository.Get(externalPaymentServiceId);

            if (payment is null)
                throw new DomainException(CheckoutExceptions.PaymentDoesNotExist);

            payment.Confirm(payedAt);

            await _paymentRepository.Update(payment);
        }

        public async Task RefusePayment(string externalPaymentServiceId)
        {
            var payment = await _paymentRepository.Get(externalPaymentServiceId);

            if (payment is null)
                throw new DomainException(CheckoutExceptions.PaymentDoesNotExist);

            payment.Refuse();

            await _paymentRepository.Update(payment);
        }
    }
}
