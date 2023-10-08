namespace FIAP.Diner.Domain.Tracking;

public enum OrderStatus
{
    WaitingForPayment,
    Received,
    InPreparation,
    Ready,
    Finished
}