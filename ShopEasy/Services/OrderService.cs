using ShopEasy.Data;
using ShopEasy.Models;
using System.Net.NetworkInformation;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    // US-030
    public void PlaceOrder(int customerId, List<CartItem> cart)
    {
        using var transaction = _context.Database.BeginTransaction();

        var order = new Order
        {
            CustomerId = customerId,
            Status = "Pending",
            OrderItems = new List<OrderItem>()
        };

        decimal total = 0;

        foreach (var item in cart)
        {
            var product = _context.Products
                .Single(p => p.ProudctId == item.ProductId);

            product.StockQuantity -= item.Quantity;

            order.OrderItems.Add(new OrderItem
            {
                productId = product.ProudctId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });

            total += item.Quantity * product.Price;
        }

        order.TotalAmount = total;

        _context.Orders.Add(order);

        _context.Payments.Add(new Payment
        {
            Order = order,
            Amount = total,
            Status = "Pending"
        });

        _context.SaveChanges();
        transaction.Commit();
    }
}