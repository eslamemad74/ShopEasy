using Microsoft.EntityFrameworkCore;
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

    //  US-031 — View Order History
    public void GetOrderHistory(int customerId)
    {
        var orders = _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderItems)
            .Include(o => o.Payment)
            .OrderByDescending(o => o.PlacedAt)
            .ToList();

        var latestOrder = _context.Orders
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.PlacedAt)
            .FirstOrDefault();

        Console.WriteLine("Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order {order.OrderId} - {order.Status} - {order.TotalAmount}");
        }

        if (latestOrder != null)
        {
            Console.WriteLine($"Latest Order: {latestOrder.OrderId}");
        }
    }


    //  US-034 — Raw SQL (Stored Procedure)
    public void GetPendingOrdersRaw()
    {
        var orders = _context.Orders
            .FromSqlRaw("EXEC GetPendingOrders")
            .ToList();

        foreach (var order in orders)
        {
            Console.WriteLine($"Order {order.OrderId} - {order.Status}");
        }
    }
}