using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Orders;

public class Order : Entity<OrderId>, IAggregateRoot
{
    #region Attributes

    private IList<OrderTracking> _trackings = new List<OrderTracking>();

    #endregion Attributes


    #region Properties

    public ShoppingCartId ShoppingCart { get; private set; }
    public WithdrawalCode WithdrawalCode { get; private set; }
    public OrderNumber Number { get; private set; }

    #endregion Properties


    #region Constructors

    private Order() { }

    #endregion Constructors


    #region Methods

    public IReadOnlyCollection<OrderTracking> Trackings => _trackings.AsReadOnly();

    public OrderStatus CurrentStatus => _trackings.OrderByDescending(s => s.When).First().OrderStatus;

    public static Order Create(OrderNumber number, ShoppingCartId shoppingCart)
    {
        if (shoppingCart.IsNullOrEmpty())
        {
            throw new DomainException(Errors.ShoppingCartRequired);
        }

        var order = new Order
        {
            Id = OrderId.New,
            Number = number,
            ShoppingCart = shoppingCart,
            WithdrawalCode = WithdrawalCode.Generate()
        };

        order.Register();

        return order;
    }

    private void Register()
    {
        _trackings.Add(new OrderTracking(Id, OrderStatus.WaitingForPayment));
    }

    public void Confirm()
    {
        if (CurrentStatus != OrderStatus.WaitingForPayment)
        {
            throw new DomainException(string.Format(Errors.CannotToConfirmOrder, CurrentStatus));
        }

        _trackings.Add(new OrderTracking(Id, OrderStatus.Confirmed));
    }

    public void Start()
    {
        if (CurrentStatus != OrderStatus.Confirmed)
        {
            throw new DomainException(string.Format(Errors.CannotToStartOrder, CurrentStatus));
        }

        _trackings.Add(new OrderTracking(Id, OrderStatus.InProgress));
    }

    public void Complete()
    {
        if (CurrentStatus != OrderStatus.InProgress)
        {
            throw new DomainException(string.Format(Errors.CannotToCompleteOrder, CurrentStatus));
        }

        _trackings.Add(new OrderTracking(Id, OrderStatus.ReadyForPickup));
    }

    public void Deliver()
    {
        if (CurrentStatus != OrderStatus.ReadyForPickup)
        {
            throw new DomainException(string.Format(Errors.CannotToDeliverOrder, CurrentStatus));
        }

        _trackings.Add(new OrderTracking(Id, OrderStatus.PickedUp));
    }

    public void Cancel()
    {
        if (CurrentStatus != OrderStatus.WaitingForPayment && CurrentStatus != OrderStatus.Confirmed)
        {
            throw new DomainException(string.Format(Errors.CannotToCancelOrder, CurrentStatus));
        }

        _trackings.Add(new OrderTracking(Id, OrderStatus.Canceled));
    }


    #endregion Methods

    public static class Errors
    {
        public static readonly string ShoppingCartRequired = "É obrigatório informar o carrinho de compras referente ao pedido.";
        public static readonly string CannotToStartOrder= "Apenas pedidos no estado 'Confirmed' podem iniciar a preparação. Estado atual do pedido: {0}";
        public static readonly string CannotToConfirmOrder = "Apenas pedidos no estado 'WaitingForPayment' podem ser confirmados. Estado atual do pedido: {0}";
        public static readonly string CannotToCompleteOrder= "Apenas pedidos no estado 'In Progress' podem ser concluídos para entrega. Estado atual do pedido: {0}";
        public static readonly string CannotToDeliverOrder= "Apenas pedidos no estado 'ReadyForPickup' podem ser liberados para entrega. Estado atual do pedido: {0}";
        public static readonly string CannotToCancelOrder= "Apenas pedidos nos estados 'WaitingForPayment' ou 'Confirmed' podem ser cancelados. Estado atual do pedido: {0}";
    }



/*
    public void UpdateStatus(OrderStatus orderStatus)
    {
        _statusHistory.Add(new OrderTracking(orderStatus));
        RaiseEvent(new OrderStatusUpdatedDomainEvent(Id, Customer, Status));
    }*/


}