namespace FIAP.Diner.Domain.Tracking;

public record TrackingStatusId
{
    public Guid Value { get; private set; }

    private TrackingStatusId() => Value = Guid.Empty;

    private TrackingStatusId(Guid value) => Value = value;

    private TrackingStatusId(string value) => Value = Guid.Parse(value);

    public static TrackingStatusId New => new();

    public static TrackingStatusId Undefined => new(Guid.Empty);

    public static implicit operator TrackingStatusId(Guid id) => new(id);

    public static implicit operator Guid(TrackingStatusId id) => new(id);

    public static implicit operator TrackingStatusId(string id) => new(id);

    public static implicit operator string(TrackingStatusId id) => new(id.Value.ToString());

    public static bool operator ==(TrackingStatusId id, string value) => id.Value.CompareTo(value) is 0;

    public static bool operator !=(TrackingStatusId id, string value) =>
        id.Value.CompareTo(value) is not 0;
}