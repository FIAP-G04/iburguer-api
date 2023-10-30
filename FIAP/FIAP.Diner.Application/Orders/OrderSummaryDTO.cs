using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Domain.Orders;

namespace FIAP.Diner.Application.Orders;

public record OrderSummaryDTO()
{
    public Guid OrderId { get; set; }

    public int OrderNumber { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public Guid ShoppingCartId { get; set; }

    public Guid? CustomerId { get; set; }

    public IList<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();

    public decimal Total { get; set; }
}

public record OrderItemDTO()
{
    public Guid OrderItemId { get; set; }

    public ushort Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Subtotal { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public Category Category { get; set; }
}