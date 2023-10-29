using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.Cart;

public class Cart : Entity<CartId>, IAggregateRoot
{
    public Cart(CustomerId2 customerId2)
    {
        Id = Guid.NewGuid();
        CustomerId2 = customerId2;
        _cartItems = new List<CartItem>();
        Closed = false;
    }

    private IList<CartItem> _cartItems { get; }
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public CustomerId2 CustomerId2 { get; private set; }

    public Price TotalPrice => _cartItems.Sum(x => x.TotalPrice);

    public bool Closed { get; private set; }

    public void AddItem(ProductId productId, Price price, ushort quantity)
    {
        var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);

        if (item is null)
            _cartItems.Add(new CartItem(productId, price, quantity));
        else
            item.Increase(quantity);
    }

    public void RemoveItem(ProductId productId, bool removeAll)
    {
        var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);

        if (item is null)
            throw new DomainException(string.Format(Errors.ItemNotPresentInCart, productId.Value));

        if (removeAll)
        {
            _cartItems.Remove(item);
        }
        else
        {
            if (item.Quantity.IsMinimum())
                _cartItems.Remove(item);
            else
                item.Decrease();
        }
    }

    public void UpdateItems(ProductId productId, Price price)
    {
        if (Closed)
            throw new DomainException(string.Format(Errors.CantUpdateClosedCart, productId.Value));

        foreach (var cartItem in _cartItems.Where(ci => ci.ProductId == productId))
            cartItem.Update(price);
    }

    public void Close()
    {
        Closed = true;
        RaiseEvent(new CartClosedDomainEvent(this));
    }

    public static class Errors
    {
        public const string ItemNotPresentInCart = "O produto de Id {0} não existe no carrinho.";

        public const string CantUpdateClosedCart =
            "O carrinho de Id {0} já está fechado e não pode ter os itens alterados.";
    }
}