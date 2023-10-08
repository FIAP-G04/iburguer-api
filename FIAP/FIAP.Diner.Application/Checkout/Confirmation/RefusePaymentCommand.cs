namespace FIAP.Diner.Application.Checkout.Confirmation;

public record RefusePaymentCommand(string ExternalPaymentServiceId);