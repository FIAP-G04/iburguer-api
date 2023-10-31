using FIAP.Diner.Application.ShoppingCarts;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.Checkout;

public class CheckoutService : ICheckoutService
{
    private readonly IPaymentRepository _repository;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public CheckoutService(IPaymentRepository repository, IShoppingCartRepository shoppingCartRepository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IPaymentRepository));
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task Checkout(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await _shoppingCartRepository.GetById(shoppingCartId, cancellation);

        if(shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        var payment = new Payment(shoppingCartId, shoppingCart.Total);

        payment.Confirm();

        await _repository.Save(payment, cancellation);
    }
}
