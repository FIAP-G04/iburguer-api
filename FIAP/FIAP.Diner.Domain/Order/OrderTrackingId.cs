namespace FIAP.Diner.Domain.Order;

public record OrderTrackingId
{
    public Guid Value { get; private set; }

    private OrderTrackingId() => Value = Guid.Empty;

    private OrderTrackingId(Guid value) => Value = value;

    private OrderTrackingId(string value) => Value = Guid.Parse(value);

    public static OrderTrackingId New => new();

    public static OrderTrackingId Undefined => new(Guid.Empty);

    public static implicit operator OrderTrackingId(Guid id) => new(id);

    public static implicit operator Guid(OrderTrackingId id) => new(id);

    public static implicit operator OrderTrackingId(string id) => new(id);

    public static implicit operator string(OrderTrackingId id) => new(id.Value.ToString());

    public static bool operator ==(OrderTrackingId id, string value) => id.Value.CompareTo(value) is 0;

    public static bool operator !=(OrderTrackingId id, string value) =>
        id.Value.CompareTo(value) is not 0;
}