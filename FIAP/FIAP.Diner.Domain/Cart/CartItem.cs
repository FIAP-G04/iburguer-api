using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Cart;

public class CartItem : Entity<CartItemId>
{
    public ProductId ProductId { get; private set; }
    public Price Price { get; private set; }
    public Quantity Quantity { get; private set; }

    public Price TotalPrice => Quantity.Value * Price;

    public CartItem(ProductId productId, Price price, ushort quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Price = price;
        Quantity = new Quantity(quantity);
    }

    public void Increase(ushort quantity = 1)
    {
        Quantity += quantity;
    }

    public void Decrease()
    {
        Quantity--;
    }

    public void Update(Price price)
    {
        Price = price;
    }
}