using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Customers
{
    public class Customer : Entity<Guid>, IAggregateRoot
    {
        public string? CPF { get; private set; }
        public string? Name { get; private set; }
        public Email? Email { get; private set; }
        public CustomerType Type { get; private set; }

        public Customer() : base()
        {
            Id = Guid.NewGuid();
            Type = CustomerType.Anonymous;
        }

        public Customer(string cpf, string name, Email email) : base()
        {
            if (string.IsNullOrEmpty(cpf))
                throw new DomainException(CustomerExceptions.CpfRequired);

            if (string.IsNullOrEmpty(name))
                throw new DomainException(CustomerExceptions.NameRequired);

            Id = Guid.NewGuid();
            CPF = cpf;
            Name = name;
            Email = email;
            Type = CustomerType.Identified;
        }
    }
}
