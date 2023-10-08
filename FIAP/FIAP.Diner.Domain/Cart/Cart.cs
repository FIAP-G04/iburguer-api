namespace FIAP.Diner.Domain.Cart;

public class Cart
{
    Combo Combo { get; set; }
    decimal Amount { get; set; }
    ushort Quantity { get; set; }
}
