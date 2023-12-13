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

    public async Task<NewPaymentDTO> Checkout(Guid shoppingCartId, CancellationToken cancellation)
    {
        var shoppingCart = await _shoppingCartRepository.GetById(shoppingCartId, cancellation);

        if(shoppingCart is null)
            throw new ShoppingCartNotFoundException(shoppingCartId);

        if (await _repository.ExistsPaymentForShoppingCart(shoppingCartId, cancellation))
            throw new PaymentAlreadyExistsForShoppingCartException(shoppingCartId);

        var payment = new Payment(shoppingCartId, shoppingCart.Total);

        await _repository.Save(payment, cancellation);

        return new(payment.Id);
    }

    public async Task<PaymentStatusDTO> GetPaymentStatus(Guid paymentId, CancellationToken cancellation)
    {
        var payment = await FindPayment(paymentId, cancellation);

        return new(payment.Id, payment.Status);
    }

    public async Task ConfirmPayment(Guid paymentId, CancellationToken cancellation)
    {
        var payment = await FindPayment(paymentId, cancellation);

        payment.Confirm();

        await _repository.Update(payment, cancellation);
    }

    public async Task RefusePayment(Guid paymentId, CancellationToken cancellation)
    {
        var payment = await FindPayment(paymentId, cancellation);

        payment.Refuse();

        await _repository.Update(payment, cancellation);
    }

    private async Task<Payment> FindPayment(Guid paymentId, CancellationToken cancellation)
    {
        var payment = await _repository.GetById(paymentId, cancellation);

        if (payment is null)
            throw new PaymentNotFoundException(paymentId);

        return payment;
    }
}
