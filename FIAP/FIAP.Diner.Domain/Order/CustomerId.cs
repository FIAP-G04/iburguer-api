namespace FIAP.Diner.Domain.Order;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId() => Value = Guid.Empty;

    private CustomerId(Guid value) => Value = value;

    private CustomerId(string value) => Value = Guid.Parse(value);

    public static CustomerId New => new();

    public static CustomerId Undefined => new(Guid.Empty);

    public static implicit operator CustomerId(Guid id) => new(id);

    public static implicit operator Guid(CustomerId id) => new(id);

    public static implicit operator CustomerId(string id) => new(id);

    public static implicit operator string(CustomerId id) => new(id.ToString());

    public static bool operator ==(CustomerId id, string value) => id.Value.CompareTo(value) is 0;

    public static bool operator !=(CustomerId id, string value) =>
        id.Value.CompareTo(value) is not 0;
}