namespace FIAP.Diner.Domain.Common;

public record Id
{
    private Id() => Value = Guid.Empty;

    public Id(Guid value) => Value = value;

    private Id(string value) => Value = Guid.Parse(value);

    public Guid Value { get; private set; }

    public static Id New => Guid.NewGuid();

    public static Id Undefined => new(Guid.Empty);

    public static implicit operator Id(Guid id) => new(id);

    public static implicit operator Guid(Id id)
    {
        var value = id.Value.ToString();

        return new Guid(value);
    }

    public static implicit operator Id(string id) => new(id);

    public static implicit operator string(Id id) => new(id.ToString());

    public static bool operator ==(Id id, string value) => id.Value.CompareTo(value) is 0;

    public static bool operator !=(Id id, string value) =>
        id.Value.CompareTo(value) is not 0;

    public bool IsNullOrEmpty() => Value == Guid.Empty || Value == null;
}