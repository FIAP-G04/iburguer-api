using System.Text.RegularExpressions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Customers
{
    public class Email
    {
        public string Value { get; private set; }

        public Email(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(email))
                throw new DomainException(CustomerExceptions.EmailRequired);

            Regex regex = new Regex(emailPattern);
            if (!regex.IsMatch(email))
                throw new DomainException(CustomerExceptions.InvalidEmail);

            Value = email;
        }
    }
}
