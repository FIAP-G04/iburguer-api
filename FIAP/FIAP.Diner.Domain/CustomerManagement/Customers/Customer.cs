using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.CustomerManagement.Customers
{
    public class Customer : Entity<CustomerId>, IAggregateRoot
    {
        public CPF CPF { get; private set; }
        public FullName Name { get; private set; }
        public Email Email { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        public DateTime LastUpdated { get; private set; }

        private Customer() { }

        public Customer(string cpf, FullName name, Email email) : base()
        {
            Id = CustomerId.New;
            CPF = cpf;
            Name = name;
            Email = email;
            RegistrationDate = DateTime.Now;
            LastUpdated = RegistrationDate;
        }

        public void Change(FullName name, Email email)
        {
            Name = name;
            Email = email;
            LastUpdated = DateTime.Now;
        }
    }
}