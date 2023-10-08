using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.CustomerManagement.Customers;

public sealed record FullName
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public static FullName From(string firstName, string lastName) => new(firstName, lastName);

    private FullName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            throw new DomainException(Errors.NameRequired);
        }

        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString() => $"{FirstName} {LastName}";

    public static bool operator ==(FullName fullName, string name) => String.Compare(fullName.ToString(), name, StringComparison.InvariantCultureIgnoreCase) is 0;

    public static bool operator !=(FullName fullName, string name) => String.Compare(fullName.ToString(), name, StringComparison.InvariantCultureIgnoreCase) is not 0;

    public static class Errors
    {
        public static readonly string NameRequired = "É obrigatório informar nome e sobrenome do cliente";
    }
}