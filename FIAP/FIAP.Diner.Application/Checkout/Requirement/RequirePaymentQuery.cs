namespace FIAP.Diner.Application.Checkout.Requirement;

public record RequirePaymentQuery(Guid OrderId, decimal Amount);