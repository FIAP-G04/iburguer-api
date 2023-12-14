using FIAP.Diner.Domain.Checkout;

namespace FIAP.Diner.Application.Checkout
{
    public record PaymentStatusDTO(Guid PaymentId, PaymentStatus Status);
}
