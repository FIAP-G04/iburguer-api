using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Checkout.Confirmation;

public class PaymentNotExistsException : DomainException
{
    public const string error = "Não existe pagamento cadastrado com o id externo {0}";

    public PaymentNotExistsException(string externalPaymentId) : base(string.Format(error, externalPaymentId))
    {

    }
}