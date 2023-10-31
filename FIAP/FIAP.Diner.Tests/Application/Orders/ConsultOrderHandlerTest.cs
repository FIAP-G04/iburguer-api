// using FIAP.Diner.Application.Orders.ConsultOrder;
// using FIAP.Diner.Domain.Orders;
//
// namespace FIAP.Diner.Tests.Application.Order;
//
// public class ConsultOrderHandlerTest
// {
//     private readonly ConsultOrderHandler _manipulator;
//     private readonly IOrderRepository _orderRepository;
//
//     public ConsultOrderHandlerTest()
//     {
//         _orderRepository = Substitute.For<IOrderRepository>();
//
//         _manipulator = new ConsultOrderHandler(_orderRepository);
//     }
//
//     [Fact]
//     public async Task ShouldGetOrderDetails()
//     {
//         var details = new OrderDetails
//         {
//             OrderId = Guid.NewGuid(),
//             CustomerId = Guid.NewGuid(),
//             TotalPrice = 11.11M,
//             Products = new List<OrderProductDetail>()
//         };
//
//         _orderRepository.GetDetails(details.OrderId, Arg.Any<CancellationToken>()).Returns(details);
//
//         var query = new ConsultOrderQuery(details.OrderId);
//
//         var result = await _manipulator.Handle(query, default);
//
//         result.Should().NotBeNull();
//         result.OrderId.Should().Be(details.OrderId);
//     }
//
//     [Fact]
//     public async Task ShouldThrowErrorWhenOrderNotFound()
//     {
//         var query = new ConsultOrderQuery(Guid.NewGuid());
//
//         _orderRepository.GetDetails(query.CustomerId, Arg.Any<CancellationToken>()).ReturnsNull();
//
//         var action = async () => await _manipulator.Handle(query, default);
//
//         await action.Should()
//             .ThrowAsync<OrderNotFoundException>()
//             .WithMessage(string.Format(OrderNotFoundException.error, query.CustomerId));
//     }
// }