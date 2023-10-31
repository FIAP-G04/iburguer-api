using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu;

public sealed record PreparationTime
{
    private PreparationTime() { }

    public PreparationTime(ushort time)
    {
        if (time <= 0)
            throw new DomainException(Errors.InvalidTime);

        if (time > 120)
            throw new DomainException(Errors.MaxTime);

        Minutes = time;
    }

    public ushort Minutes { get; private set; }

    public override string ToString() => Minutes.ToString();

    public static implicit operator ushort(PreparationTime time) => time.Minutes;

    public static implicit operator PreparationTime(ushort minutes) => new(minutes);

    public static class Errors
    {
        public static readonly string InvalidTime = "O tempo de preparação não pode ser igual a zero ou negativo";

        public static readonly string MaxTime = "O tempo máximo de preparação não pode ultrapassar 120 minutos";
    }
}