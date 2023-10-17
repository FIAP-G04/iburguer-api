using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Domain.Cart;

namespace FIAP.Diner.Application.Cart;

public class CartManagementHandler : ICommandHandler<AddItemToCartCommand>,
                                     ICommandHandler<RemoveItemFromCartCommand>,
                                     ICommandHandler<UpdateCartItemProductInformation>,
                                     ICommandHandler<CloseCartCommand>,
                                     IQueryHandler<GetCartItemsQuery, CartDetails>
{
    private readonly ICartRepository _cartRepository;

    public CartManagementHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task Handle(AddItemToCartCommand command, CancellationToken cancellation)
    {
        var cart = await _cartRepository.Get(command.CustomerId, cancellation);

        if (cart == null)
        {
            cart = new Domain.Cart.Cart(command.CustomerId);

            await _cartRepository.Save(cart, cancellation);
        }

        cart.AddItem(command.ProductId, command.Price, command.Quantity);

        await _cartRepository.Update(cart, cancellation);
    }

    public async Task Handle(RemoveItemFromCartCommand command, CancellationToken cancellation)
    {
        var cart = await _cartRepository.Get(command.CustomerId, cancellation);

        if (cart == null)
        {
            throw new CartNotFoundException(command.CustomerId);
        }

        cart.RemoveItem(command.ProductId, command.RemoveAll);

        await _cartRepository.Update(cart, cancellation);
    }

    public async Task Handle(UpdateCartItemProductInformation command,
        CancellationToken cancellation)
    {
        var carts = await _cartRepository.GetByProductInCart(command.ProductId, cancellation);

        foreach (var cart in carts)
        {
            cart.UpdateItems(command.ProductId, command.Price);

            await _cartRepository.Update(cart, cancellation);
        }
    }

    public async Task<CartDetails> Handle(GetCartItemsQuery query, CancellationToken cancellation)
    {
        var cartDetail = await _cartRepository.GetDetailed(query.CustomerId, cancellation);

        if (cartDetail == null)
        {
            throw new CartNotFoundException(query.CustomerId);
        }

        return cartDetail;
    }

    public async Task Handle(CloseCartCommand command, CancellationToken cancellation)
    {
        var cart = await _cartRepository.Get(command.CustomerId, cancellation);

        if (cart == null)
        {
            throw new CartNotFoundException(command.CustomerId);
        }

        cart.Close();

        await _cartRepository.Update(cart, cancellation);
    }
}