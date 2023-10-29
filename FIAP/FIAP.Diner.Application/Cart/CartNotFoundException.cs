using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Cart;

public class CartNotFoundException : DomainException
{
    public const string error = "Não existe carrinho para o cliente {0}";

    public CartNotFoundException(Guid customerId) : base(
        string.Format(error, customerId.ToString()))
    {
    }
}