namespace FIAP.Diner.Domain.Checkout;

public record PaymentId
{
    private PaymentId() => Value = Guid.Empty;

    private PaymentId(Guid value) => Value = value;

    private PaymentId(string value) => Value = Guid.Parse(value);
    public Guid Value { get; }

    public static PaymentId New => new();

    public static PaymentId Undefined => new(Guid.Empty);

    public static implicit operator PaymentId(Guid id) => new(id);

    public static implicit operator Guid(PaymentId id) => new(id);

    public static implicit operator PaymentId(string id) => new(id);

    public static implicit operator string(PaymentId id) => new(id.ToString());

    public static bool operator ==(PaymentId id, string value) => id.Value.CompareTo(value) is 0;

    public static bool operator !=(PaymentId id, string value) =>
        id.Value.CompareTo(value) is not 0;
}