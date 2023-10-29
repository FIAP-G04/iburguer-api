namespace FIAP.Diner.Domain.Cart;

public record CartItemId
{
    private CartItemId() => Value = Guid.Empty;

    private CartItemId(Guid value) => Value = value;

    private CartItemId(string value) => Value = Guid.Parse(value);
    public Guid Value { get; }

    public static CartItemId New => new();

    public static CartItemId Undefined => new(Guid.Empty);

    public static implicit operator CartItemId(Guid id) => new(id);

    public static implicit operator Guid(CartItemId id) => new(id);

    public static implicit operator CartItemId(string id) => new(id);

    public static implicit operator string(CartItemId id) => new(id.ToString());

    public static bool operator ==(CartItemId id, string value) => id.Value.CompareTo(value) is 0;

    public static bool operator !=(CartItemId id, string value) =>
        id.Value.CompareTo(value) is not 0;
}