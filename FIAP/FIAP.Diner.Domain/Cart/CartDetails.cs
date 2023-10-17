namespace FIAP.Diner.Domain.Cart;

public class CartDetails
{
    public Guid CustomerId { get; set; }
    public IEnumerable<CartItemDetails> CartItems { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CartItemDetails
{
    public Guid ProductId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Quantity Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}