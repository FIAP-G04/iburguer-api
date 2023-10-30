namespace FIAP.Diner.Domain.Orders;

public class OrderDetails
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }
    public IEnumerable<OrderProductDetail> Products { get; set; }
}

public class OrderProductDetail
{
    public Guid ProductId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}