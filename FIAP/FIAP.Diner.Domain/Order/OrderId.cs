namespace FIAP.Diner.Domain.Order
{
    public record OrderId
    {
        public Guid Value { get; private set; }

        private OrderId() => Value = Guid.Empty;

        private OrderId(Guid value) => Value = value;

        private OrderId(string value) => Value = Guid.Parse(value);

        public static OrderId New => new();

        public static OrderId Undefined => new(Guid.Empty);

        public static implicit operator OrderId(Guid id) => new(id);

        public static implicit operator Guid(OrderId id) => new(id);

        public static implicit operator OrderId(string id) => new(id);

        public static implicit operator string(OrderId id) => new(id.Value.ToString());

        public static bool operator ==(OrderId id, string value) => id.Value.CompareTo(value) is 0;

        public static bool operator !=(OrderId id, string value) =>
            id.Value.CompareTo(value) is not 0;
    }
}
