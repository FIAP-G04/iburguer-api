using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Checkout
{
    public class PaymentAlreadyExistsForShoppingCartException : DomainException
    {
        public const string error = "O carrinho com Id {0} já possui pagamento criado";

        public PaymentAlreadyExistsForShoppingCartException(Guid paymentId) : base(string.Format(error, paymentId)) { }
    }
}
