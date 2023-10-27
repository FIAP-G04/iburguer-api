using FIAP.Diner.Application.Order.Tracking;
using FIAP.Diner.Domain.Cart;
using FIAP.Diner.Domain.Order;

namespace FIAP.Diner.Tests.Application.Order
{
    public class OrderRegisterEventHandlerTest
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        private readonly OrderRegisterEventHandler _manipulator;

        public OrderRegisterEventHandlerTest()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _cartRepository = Substitute.For<ICartRepository>();

            _manipulator = new(_orderRepository, _cartRepository);
        }

        [Fact]
        public async Task ShouldRegisterOrder()
        {
            var cartId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            var @event = new PaymentConfirmedDomainEvent(cartId);

            _cartRepository.GetCustomerId(cartId, Arg.Any<CancellationToken>()).Returns(customerId);
            _orderRepository.GetNextWithdrawCode(Arg.Any<CancellationToken>()).Returns("ABC-123");

            await _manipulator.Handle(@event, default);

            await _orderRepository.Received()
                .Save(Arg.Is<Diner.Domain.Order.Order>(o =>
                    o.CartId.Value == cartId &&
                    o.CustomerId.Value == customerId &&
                    o.WithdrawCode == "ABC-123"), Arg.Any<CancellationToken>());
        }
    }
}
