namespace FIAP.Diner.Domain.Common
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }

        public DomainException(string message, params object?[] values) : base(string.Format(message, values)) { }
    }
}
