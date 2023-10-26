namespace FIAP.Diner.Domain.Checkout
{
    public record CartId
    {
        public Guid Value { get; }

        private CartId() => Value = Guid.Empty;

        private CartId(Guid value) => Value = value;

        private CartId(string value) => Value = Guid.Parse(value);

        public static CartId New => new();

        public static CartId Undefined => new(Guid.Empty);

        public static implicit operator CartId(Guid id) => new(id);

        public static implicit operator Guid(CartId id) => new(id);

        public static implicit operator CartId(string id) => new(id);

        public static implicit operator string(CartId id) => new(id.ToString());

        public static bool operator ==(CartId id, string value) => id.Value.CompareTo(value) is 0;

        public static bool operator !=(CartId id, string value) =>
            id.Value.CompareTo(value) is not 0;
    }
}
