namespace FIAP.Diner.Domain.Checkout
{
    public static class CheckoutExceptions
    {
        public static readonly string PaymentDoesNotExist = "Payment does not exist";
        public static readonly string ErrorGeneratingPayment = "There was an error when generating this payment";
    }
}
