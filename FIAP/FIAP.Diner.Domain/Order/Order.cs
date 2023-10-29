using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Order;

public class Order : Entity<OrderId>, IAggregateRoot
{
    public Order(CartId cartId, CustomerId3 customerId3)
    {
        Id = Guid.NewGuid();

        CartId = cartId;
        CustomerId3 = customerId3;

        _statusHistory = new List<OrderTracking> { new(OrderStatus.WaitingForPayment) };
    }

    private IList<OrderTracking> _statusHistory { get; }

    public IReadOnlyCollection<OrderTracking> StatusHistory =>
        _statusHistory.AsReadOnly();

    public OrderTracking Status =>
        _statusHistory.OrderByDescending(s => s.DateTime).FirstOrDefault();

    public CartId CartId { get; private set; }
    public CustomerId3 CustomerId3 { get; }
    public string WithdrawCode { get; private set; }

    public void AddWithdrawCode(string withdrawCode) => WithdrawCode = withdrawCode;

    public void UpdateStatus(OrderStatus orderStatus)
    {
        _statusHistory.Add(new OrderTracking(orderStatus));
        RaiseEvent(new OrderStatusUpdatedDomainEvent(Id, CustomerId3, Status));
    }
}