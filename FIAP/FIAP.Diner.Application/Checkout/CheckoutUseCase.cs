using FIAP.Diner.Application.ShoppingCarts;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.Checkout;

public interface ICheckoutUseCase
{
    Task<NewPaymentDTO> Checkout(Guid shoppingCartId, CancellationToken cancellation);
}

public class CheckoutUseCase : ICheckoutUseCase
{
    private readonly IPaymentRepository _repository;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public CheckoutUseCase(IPaymentRepository repository, IShoppingCartRepository shoppingCartRepository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IPaymentRepository));
        ArgumentNullException.ThrowIfNull(repository, nameof(IShoppingCartRepository));

        _repository = repository;
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task<NewPaymentDTO> Checkout(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await _shoppingCartRepository.GetById(shoppingCartId, cancellation);

        if(shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        var existsPayment = await _repository.ExistsPaymentForShoppingCart(shoppingCartId, cancellation);

        if (existsPayment)
            throw new PaymentAlreadyExistsForShoppingCartException(shoppingCartId);

        var payment = new Payment(shoppingCartId, shoppingCart.Total);

        await _repository.Save(payment, cancellation);

        return new(payment.Id);
    }
}