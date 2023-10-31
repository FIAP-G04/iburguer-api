using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.ShoppingCarts;

public class ShoppingCartNotFoundException : DomainException
{
    public const string error = "Não foi encontrado nenhum carrinho de compras com o Id {0}";

    public ShoppingCartNotFoundException(Guid shoppingCartId) : base(string.Format(error,
        shoppingCartId.ToString()))
    {
    }
}