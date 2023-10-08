namespace FIAP.Diner.Domain.Tracking
{
    public record OrderTrackingId()
    {
        public Guid Value = Guid.NewGuid();
    }
}
