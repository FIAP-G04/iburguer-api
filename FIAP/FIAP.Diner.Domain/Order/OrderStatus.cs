namespace FIAP.Diner.Domain.Order;

public enum OrderStatus
{
    WaitingForPayment,
    Received,
    InPreparation,
    Ready,
    Finished
}