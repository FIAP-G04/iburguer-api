namespace FIAP.Diner.Domain.Menu
{
    public record ImageURLId
    {
        public Guid Value { get; }

        private ImageURLId() => Value = Guid.Empty;

        private ImageURLId(Guid value) => Value = value;

        private ImageURLId(string value) => Value = Guid.Parse(value);

        public static ImageURLId New => new();

        public static ImageURLId Undefined => new(Guid.Empty);

        public static implicit operator ImageURLId(Guid id) => new(id);

        public static implicit operator Guid(ImageURLId id) => new(id);

        public static implicit operator ImageURLId(string id) => new(id);

        public static implicit operator string(ImageURLId id) => new(id.ToString());

        public static bool operator ==(ImageURLId id, string value) => id.Value.CompareTo(value) is 0;

        public static bool operator !=(ImageURLId id, string value) =>
            id.Value.CompareTo(value) is not 0;
    }
}
