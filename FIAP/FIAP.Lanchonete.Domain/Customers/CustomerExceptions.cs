namespace FIAP.Diner.Domain.Customers
{
    public static class CustomerExceptions
    {
        public readonly static string InvalidEmail = "The email provided is invalid";
        public readonly static string EmailRequired = "Email is required";
        public readonly static string CpfRequired = "CPF is required";
        public readonly static string NameRequired = "Name is required";
        public readonly static string CustomerWithCPFDoesNotExist = "Customer with provided CPF does not exist";
    }
}
