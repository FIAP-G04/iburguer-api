namespace FIAP.Diner.Domain.Cart;

public record CustomerId2
{
    private CustomerId2() => Value = Guid.Empty;

    private CustomerId2(Guid value) => Value = value;

    private CustomerId2(string value) => Value = Guid.Parse(value);
    public Guid Value { get; }

    public static CustomerId2 New => new();

    public static CustomerId2 Undefined => new(Guid.Empty);

    public static implicit operator CustomerId2(Guid id) => new(id);

    public static implicit operator Guid(CustomerId2 id2) => new(id2);

    public static implicit operator CustomerId2(string id) => new(id);

    public static implicit operator string(CustomerId2 id2) => new(id2.ToString());

    public static bool operator ==(CustomerId2 id2, string value) => id2.Value.CompareTo(value) is 0;

    public static bool operator !=(CustomerId2 id2, string value) =>
        id2.Value.CompareTo(value) is not 0;
}