using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.ShoppingCarts;

public class CartItem : Entity<CartItemId>
{
    #region Properties

    public ShoppingCartId ShoppingCart { get; private set; }
    public ProductId Product { get; private set; }
    public Price Price { get; private set; }
    public Quantity Quantity { get; private set; }

    #endregion Properties


    #region Constructor

    private CartItem() {}

    public CartItem(ShoppingCartId shoppingCart, ProductId product, Price price, Quantity quantity)
    {
        Id = CartItemId.New;
        ShoppingCart = shoppingCart;
        Product = product;
        Price = price;
        Quantity = quantity;
    }

    #endregion Constructor


    #region Methods

    public Price Subtotal => Quantity * Price;

    public void UpdatePrice(Price price) => Price = price;

    #endregion Methods
}