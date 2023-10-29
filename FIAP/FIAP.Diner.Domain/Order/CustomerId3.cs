namespace FIAP.Diner.Domain.Order;

public record CustomerId3
{
    private CustomerId3() => Value = Guid.Empty;

    private CustomerId3(Guid value) => Value = value;

    private CustomerId3(string value) => Value = Guid.Parse(value);
    public Guid Value { get; }

    public static CustomerId3 New => new();

    public static CustomerId3 Undefined => new(Guid.Empty);

    public static implicit operator CustomerId3(Guid id) => new(id);

    public static implicit operator Guid(CustomerId3 id3) => new(id3);

    public static implicit operator CustomerId3(string id) => new(id);

    public static implicit operator string(CustomerId3 id3) => new(id3.ToString());

    public static bool operator ==(CustomerId3 id3, string value) => id3.Value.CompareTo(value) is 0;

    public static bool operator !=(CustomerId3 id3, string value) =>
        id3.Value.CompareTo(value) is not 0;
}