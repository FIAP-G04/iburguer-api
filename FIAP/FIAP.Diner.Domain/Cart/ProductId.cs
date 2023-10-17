namespace FIAP.Diner.Domain.Cart;

public record ProductId
{
    public Guid Value { get; }

    private ProductId() => Value = Guid.Empty;

    private ProductId(Guid value) => Value = value;

    private ProductId(string value) => Value = Guid.Parse(value);

    public static ProductId New => new();

    public static ProductId Undefined => new(Guid.Empty);

    public static implicit operator ProductId(Guid id) => new(id);

    public static implicit operator Guid(ProductId id) => new(id);

    public static implicit operator ProductId(string id) => new(id);

    public static implicit operator string(ProductId id) => new(id.ToString());

    public static bool operator ==(ProductId id, string value) => id.Value.CompareTo(value) is 0;

    public static bool operator !=(ProductId id, string value) =>
        id.Value.CompareTo(value) is not 0;
}