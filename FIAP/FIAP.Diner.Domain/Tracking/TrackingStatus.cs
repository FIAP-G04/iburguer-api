namespace FIAP.Diner.Domain.Tracking;

public record TrackingStatus(OrderStatus OrderStatus)
{
    public DateTime DateTime = DateTime.Now;
}