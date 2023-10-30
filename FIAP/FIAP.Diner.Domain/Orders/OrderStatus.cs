namespace FIAP.Diner.Domain.Orders;

public enum OrderStatus
{
    WaitingForPayment,
    Confirmed,
    InProgress,
    ReadyForPickup,
    PickedUp,
    Canceled
}