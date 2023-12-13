using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Checkout
{
    public class PaymentNotFoundException : DomainException
    {
        public const string error = "Não foi possível contrar pagamento com o Id {0}";

        public PaymentNotFoundException(Guid paymentId) : base(string.Format(error, paymentId)) { }
    }
}
