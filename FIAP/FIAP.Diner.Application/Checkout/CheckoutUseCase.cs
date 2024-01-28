using FIAP.Diner.Application.Orders;
using FIAP.Diner.Application.ShoppingCarts;
using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Domain.ShoppingCarts;

namespace FIAP.Diner.Application.Checkout;

public interface ICheckoutUseCase
{
    Task<CheckoutRequestedDTO> Checkout(Guid shoppingCartId, CancellationToken cancellation);
}

public class CheckoutUseCase : ICheckoutUseCase
{
    private readonly IPaymentRepository _repository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IRegisterOrderUseCase _registerOrderUseCase;

    public CheckoutUseCase(IPaymentRepository repository, IShoppingCartRepository shoppingCartRepository, IRegisterOrderUseCase registerOrderUseCase)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(IPaymentRepository));
        ArgumentNullException.ThrowIfNull(shoppingCartRepository, nameof(IShoppingCartRepository));
        ArgumentNullException.ThrowIfNull(registerOrderUseCase, nameof(IRegisterOrderUseCase));

        _repository = repository;
        _shoppingCartRepository = shoppingCartRepository;
        _registerOrderUseCase = registerOrderUseCase;
    }

    public async Task<CheckoutRequestedDTO> Checkout(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await _shoppingCartRepository.GetById(shoppingCartId, cancellation);

        if(shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        var existsPayment = await _repository.ExistsPaymentForShoppingCart(shoppingCartId, cancellation);

        if (existsPayment)
            throw new PaymentAlreadyExistsForShoppingCartException(shoppingCartId);

        var payment = new Payment(shoppingCartId, shoppingCart.Total);

        await _repository.Save(payment, cancellation);

        var orderNumber = await _registerOrderUseCase.RegisterOrder(shoppingCartId, cancellation);

        return new(orderNumber, payment.Id);
    }
}