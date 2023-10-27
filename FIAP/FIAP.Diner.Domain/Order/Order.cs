using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Order;

public class Order : Entity<OrderId>, IAggregateRoot
{
    private IList<OrderTracking> _statusHistory { get; }

    public IReadOnlyCollection<OrderTracking> StatusHistory =>
        _statusHistory.AsReadOnly();

    public OrderTracking Status => _statusHistory.OrderByDescending(s => s.DateTime).FirstOrDefault();
    public CartId CartId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public string WithdrawCode { get; private set; }

    public Order(CartId cartId, CustomerId customerId)
    {
        Id = Guid.NewGuid();

        CartId = cartId;
        CustomerId = customerId;

        _statusHistory = new List<OrderTracking>
        {
            new OrderTracking(OrderStatus.WaitingForPayment)
        };
    }

    public void AddWithdrawCode(string withdrawCode) => WithdrawCode = withdrawCode;

    public void UpdateStatus(OrderStatus orderStatus)
    {
        _statusHistory.Add(new OrderTracking(orderStatus));
        RaiseEvent(new OrderStatusUpdatedDomainEvent(Id, CustomerId, Status));
    }
}