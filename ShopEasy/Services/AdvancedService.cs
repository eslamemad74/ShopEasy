using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Services
{
    public class AdvancedService
    {
        private readonly AppDbContext _context;

        public AdvancedService(AppDbContext context)
        {
            _context = context;
        }

        //  US-050 — Lazy Loading
        public void LazyLoadReviews(int productId)
        {
            var product = _context.Products.Find(productId);

            Console.WriteLine($"Product: {product.Name}");

            // هنا Lazy Loading هيشتغل تلقائي
            var reviews = product.Reviews;

            foreach (var review in reviews)
            {
                Console.WriteLine($"Rating: {review.Rating}");
            }
        }

        // US-051 — Split Query
        public void GetCustomerFullData(int customerId)
        {
            var customer = _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                .Include(c => c.Reviews)
                .AsSplitQuery()
                .SingleOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }

            Console.WriteLine(customer.FullName);
            Console.WriteLine($"Orders: {customer.Orders.Count}");
            Console.WriteLine($"Reviews: {customer.Reviews.Count}");
        }

        //  US-052 — Customers with no orders
        public void GetCustomersWithNoOrders()
        {
            var customers = _context.Customers
                .Where(c => !c.Orders.Any())
                .Select(c => new
                {
                    c.FullName,
                    c.Email
                })
                .ToList();

            foreach (var c in customers)
            {
                Console.WriteLine($"{c.FullName} - {c.Email}");
            }
        }

        //  US-053 — Products ranked by total sold
        public void GetTopSellingProducts()
        {
            var result = _context.Products
                .Join(_context.OrderItems,
                    p => p.ProudctId,
                    oi => oi.productId,
                    (p, oi) => new { p, oi })
                .GroupBy(x => new { x.p.ProudctId, x.p.Name })
                .Select(g => new
                {
                    ProductName = g.Key.Name,
                    TotalSold = g.Sum(x => x.oi.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.ProductName} - Sold: {item.TotalSold}");
            }
        }
    }
}
