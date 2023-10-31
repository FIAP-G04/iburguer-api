// using FIAP.Diner.Application.Orders;
// using FIAP.Diner.Domain.Orders;
// using FIAP.Diner.Domain.ShoppingCarts;
//
// namespace FIAP.Diner.Tests.Application.Order;
//
// public class OrderRegisterEventHandlerTest
// {
//     private readonly IShoppingCartRepository shoppingCartRepository;
//
//     private readonly OrderRegisterEventHandler _manipulator;
//     private readonly IOrderRepository _orderRepository;
//
//     public OrderRegisterEventHandlerTest()
//     {
//         _orderRepository = Substitute.For<IOrderRepository>();
//         shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
//
//         _manipulator = new OrderRegisterEventHandler(_orderRepository, shoppingCartRepository);
//     }
//
//     [Fact]
//     public async Task ShouldRegisterOrder()
//     {
//         var cartId = Guid.NewGuid();
//         var customerId = Guid.NewGuid();
//
//         var @event = new PaymentConfirmedDomainEvent(cartId);
//
//         shoppingCartRepository.GetCustomerId(cartId, Arg.Any<CancellationToken>()).Returns(customerId);
//         _orderRepository.GetNextWithdrawCode(Arg.Any<CancellationToken>()).Returns("ABC-123");
//
//         await _manipulator.Handle(@event, default);
//
//         await _orderRepository.Received()
//             .Save(Arg.Is<Diner.Domain.Orders.Order>(o =>
//                 o.CartId.Value == cartId &&
//                 o.CustomerId3.Value == customerId &&
//                 o.WithdrawCode == "ABC-123"), Arg.Any<CancellationToken>());
//     }
// }