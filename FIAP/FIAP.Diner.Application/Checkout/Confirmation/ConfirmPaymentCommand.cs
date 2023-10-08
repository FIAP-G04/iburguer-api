namespace FIAP.Diner.Application.Checkout.Confirmation;

public record ConfirmPaymentCommand(string ExternalPaymentServiceId, DateTime PayedAt);