using FIAP.Diner.Application.Common;
using FIAP.Diner.Application.Orders;
using FIAP.Diner.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Diner.Infrastructure.Data.Modules.Orders;

public class OrderRetriever : IOrderRetriever
{
    private readonly Context _context;

    public OrderRetriever(Context context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<PaginatedList<OrderSummaryDTO>> GetPagedOrdersAsync(int page, int limit, CancellationToken cancellation)
    {
        if (limit > 50) limit = 50;
        if (limit < 0) limit = 10;
        if (page < 1) page = 1;

        var query = _context.Orders.Include(o => o.Trackings)
            .OrderByDescending(order => order.Number)
            .Skip((page - 1) * limit)
            .Take(limit)
            .Join(
                _context.ShoppingCarts.Include(s => s.Items),
                order => order.ShoppingCart,
                cart => cart.Id,
                (order, cart) => new OrderSummaryDTO
                {
                    OrderId = order.Id,
                    OrderNumber = order.Number,
                    OrderStatus = order.CurrentStatus,
                    ShoppingCartId = cart.Id,
                    CustomerId = cart.Customer,
                    Total = cart.Total.Amount,
                    Items = _context.CartItems
                        .Where(cartItem => cartItem.ShoppingCart == cart.Id)
                        .Join(
                            _context.Products,
                            cartItem => cartItem.Product,
                            product => product.Id,
                            (cartItem, product) => new OrderItemDTO
                            {
                                OrderItemId = cartItem.Id,
                                Quantity = cartItem.Quantity.Value,
                                Price = cartItem.Price.Amount,
                                Subtotal = cartItem.Price.Amount * cartItem.Quantity.Value,
                                ProductId = cartItem.Product,
                                ProductName = product.Name,
                                Category = product.Category
                            })
                        .ToList()
                });

        var total = await _context.Orders.CountAsync();

        var paginatedData = await query.ToListAsync();

        var paginatedList = new PaginatedList<OrderSummaryDTO>
        {
            Total = total,
            Page= page,
            Limit = limit,
            Items = paginatedData
        };

        return paginatedList;
    }

    public async Task<PaginatedList<OrderSummaryDTO>> GetOrderQueueAsync(int page, int limit, CancellationToken cancellation)
    {
        if (limit > 50) limit = 50;
        if (limit < 0) limit = 10;
        if (page < 1) page = 1;

        var query = _context.Orders.Include(o => o.Trackings)
            .Where(o => new List<OrderStatus>()
                {
                    OrderStatus.Confirmed ,
                    OrderStatus.InProgress
                }
            .Contains( o.Trackings.OrderByDescending(t => t.When).First().OrderStatus))
            .OrderBy(order => order.Number)
            .Skip((page - 1) * limit)
            .Take(limit)
            .Join(
                _context.ShoppingCarts.Include(s => s.Items),
                order => order.ShoppingCart,
                cart => cart.Id,
                (order, cart) => new OrderSummaryDTO
                {
                    OrderId = order.Id,
                    OrderNumber = order.Number,
                    OrderStatus = order.CurrentStatus,
                    ShoppingCartId = cart.Id,
                    CustomerId = cart.Customer,
                    Total = cart.Total.Amount,
                    Items = _context.CartItems
                        .Where(cartItem => cartItem.ShoppingCart == cart.Id)
                        .Join(
                            _context.Products,
                            cartItem => cartItem.Product,
                            product => product.Id,
                            (cartItem, product) => new OrderItemDTO
                            {
                                OrderItemId = cartItem.Id,
                                Quantity = cartItem.Quantity.Value,
                                Price = cartItem.Price.Amount,
                                Subtotal = cartItem.Price.Amount * cartItem.Quantity.Value,
                                ProductId = cartItem.Product,
                                ProductName = product.Name,
                                Category = product.Category
                            })
                        .ToList()
                });

        var total = await _context.Orders.CountAsync();

        var paginatedData = await query.ToListAsync();

        var paginatedList = new PaginatedList<OrderSummaryDTO>
        {
            Total = total,
            Page= page,
            Limit = limit,
            Items = paginatedData
        };

        return paginatedList;
    }
}