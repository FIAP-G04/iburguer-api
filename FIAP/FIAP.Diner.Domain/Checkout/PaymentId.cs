namespace FIAP.Diner.Domain.Checkout
{
    public record PaymentId()
    {
        public Guid Value = Guid.NewGuid();
    }
}
