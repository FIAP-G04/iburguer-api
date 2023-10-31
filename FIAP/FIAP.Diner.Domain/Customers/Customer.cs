using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Customers;

public class Customer : Entity<CustomerId>, IAggregateRoot
{
    public CPF CPF { get; private set; }

    public PersonName Name { get; private set; }

    public Email Email { get; private set; }

    public DateTime RegistrationDate { get; }

    public DateTime LastUpdated { get; private set; }

    private Customer() { }

    public Customer(string cpf, PersonName name, Email email)
    {
        Id = CustomerId.New;
        CPF = cpf;
        Name = name;
        Email = email;
        RegistrationDate = DateTime.Now;
        LastUpdated = RegistrationDate;
    }

    public void Change(PersonName name, Email email)
    {
        Name = name;
        Email = email;
        LastUpdated = DateTime.Now;
    }
}