using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Checkout;

public class Payment : Entity<PaymentId>, IAggregateRoot
{
    #region Properties

    public ShoppingCartId ShoppingCart { get; private set; }
    public Price Amount { get; private set; }
    public DateTime? PayedAt { get; private set; }
    public DateTime? RefusedAt { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }

    #endregion Properties


    #region Constructors

    private Payment() { }

    public Payment(ShoppingCartId shoppingCart, Price amount)
    {
        Id = PaymentId.New;
        ShoppingCart = shoppingCart;
        Amount = amount;
        Status = PaymentStatus.Pending;
        Method = PaymentMethod.QRCode;
    }

    #endregion Constructors


    #region Methods

    public bool Confirmed => Status is PaymentStatus.Received && PayedAt is not null;

    public void Confirm()
    {
        if (Status != PaymentStatus.Pending)
        {
            throw new DomainException(string.Format(Errors.CannotToConfirmPayment, Status));
        }

        PayedAt = DateTime.Now;
        Status = PaymentStatus.Received;

        RaiseEvent(new PaymentConfirmedDomainEvent(ShoppingCart));
    }

    public void Refuse()
    {
        if (Status != PaymentStatus.Pending)
        {
            throw new DomainException(string.Format(Errors.CannotToRefusePayment, Status));
        }

        RefusedAt = DateTime.Now;
        Status = PaymentStatus.Refused;

        RaiseEvent(new PaymentRefusedDomainEvent(ShoppingCart));
    }

    #endregion Methods


    public static class Errors
    {
        public static readonly string CannotToRefusePayment= "Apenas pedidos no estado 'Pending' podem ser recusados. Estado atual do pedido: {0}";
        public static readonly string CannotToConfirmPayment = "Apenas pagamentos no estado 'Pending' podem ser confirmados. Estado atual do pagamento: {0}";
    }
}