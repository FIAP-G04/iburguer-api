namespace FIAP.Diner.Domain.Abstractions
{
    public class DomainException : Exception
    {
        public DomainException(string message, params object?[] values) : base(string.Format(message, values)) { }
    }
}
