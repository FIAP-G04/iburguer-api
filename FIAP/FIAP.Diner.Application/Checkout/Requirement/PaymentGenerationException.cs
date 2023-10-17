using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Checkout.Requirement;

public class PaymentGenerationException : DomainException
{
    public const string error = "Ocorreu um erro ao gerar o pagamento para o pedido {0}";

    public PaymentGenerationException(Guid orderId) : base(string.Format(error, orderId.ToString()))
    {

    }
}