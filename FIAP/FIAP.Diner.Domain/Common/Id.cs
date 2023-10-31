namespace FIAP.Diner.Domain.Common;

public class Id
{
    #region Properties

    public Guid Value { get; private set; }

    #endregion

    #region Constructors

    private Id() => Value = Guid.Empty;

    public Id(Guid value) => Value = value;

    private Id(string value) => Value = Guid.Parse(value);


    #endregion

    #region Methods

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

    public bool IsNullOrEmpty() => Value == Guid.Empty || Value == null;

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is Id otherId)
        {
            return Value == otherId.Value;
        }
        if (obj is Guid otherGuid)
        {
            return Value == otherGuid;
        }
        if (obj is string otherString)
        {
            return Value.ToString() == otherString;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    #endregion



}