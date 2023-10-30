using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Domain.ShoppingCarts;

public class ShoppingCart : Entity<ShoppingCartId>, IAggregateRoot
{
    #region Attributes

    private IList<CartItem> _items = new List<CartItem>();

    #endregion Attributes


    #region Properties

    public CustomerId Customer { get; private set; }

    public bool Closed { get; private set; }

    #endregion Properties


    #region Constructors

    private ShoppingCart() { }

    #endregion Constructors


    #region Methods

    public static ShoppingCart GenerateAnonymousShoppingCart()
    {
        return new ShoppingCart { Id = ShoppingCartId.New, Closed = false };
    }

    public static ShoppingCart GenerateCustomerShoppingCart(CustomerId customer)
    {
        //Validar Customer

        return new ShoppingCart { Id = ShoppingCartId.New, Customer = customer, Closed = false };
    }

    public Price Total => _items.Sum(x => x.Subtotal);

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public void AddCartItem(ProductId product, Price price, Quantity quantity)
    {
        CheckIfTheShoppingCartIsClosed();

        var item = GetCartItemByProduct(product);

        if (item is null)
        {
            _items.Add(new CartItem(Id, product, price, quantity));
        }
        else
        {
            item.Quantity.Increment(quantity);
        }
    }

    public void Clear() => _items.Clear();

    public void RemoveCartItem(CartItemId cartItemId)
    {
        CheckIfTheShoppingCartIsClosed();

        _items.Remove(GetCartItemById(cartItemId));
    }

    public void IncrementTheQuantityOfTheCartItem(CartItemId cartItemId, Quantity quantity)
    {
        CheckIfTheShoppingCartIsClosed();

        GetCartItemById(cartItemId).Quantity.Increment(quantity);
    }

    public void DecrementTheQuantityOfTheCartItem(CartItemId cartItemId, Quantity quantity)
    {
        CheckIfTheShoppingCartIsClosed();

        GetCartItemById(cartItemId).Quantity.Decrement(quantity);
    }

    public void Close()
    {
        if (!Items.Any())
        {
            throw new DomainException(string.Format(Errors.UnableToCloseWithoutAnyCartItems, Id));
        }

        Closed = true;
        RaiseEvent(new CartClosedDomainEvent(Id));
    }

    public void UpdateItemPriceThroughProduct(ProductId product, Price price)
    {
        CheckIfTheShoppingCartIsClosed();

        GetCartItemByProduct(product)?.UpdatePrice(price);
    }

    private void CheckIfTheShoppingCartIsClosed()
    {
        if (Closed)
            throw new DomainException(string.Format(Errors.CannotUpdateClosedCart, Id));
    }

    public CartItem GetCartItemById(CartItemId id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);

        if (item is null)
            throw new DomainException(string.Format(Errors.ItemNotPresentInCart, id));

        return item;
    }

    private CartItem? GetCartItemByProduct(ProductId product) => _items.FirstOrDefault(i => i.Product == product);

    #endregion Methods

    public static class Errors
    {
        public const string ItemNotPresentInCart = "O produto de Id {0} não existe no carrinho.";

        public const string CannotUpdateClosedCart =
            "O carrinho de compras Id {0} já está fechado e não pode ser modificado.";

        public const string UnableToCloseWithoutAnyCartItems = "Não é possível fechar um carrinho de compras Id={0} sem nenhum item";
    }
}