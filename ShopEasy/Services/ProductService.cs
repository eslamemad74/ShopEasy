using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Services
{
   
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // US-020
        public void GetAllProducts()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name
                })
                .AsNoTracking()
                .ToList();

            foreach (var p in products)
                Console.WriteLine($"{p.Name} - {p.Price}");
        }

        // US-021
        public void Search(string keyword, string category)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Where(p =>
                    p.Name.Contains(keyword) ||
                    p.Category.Name.Contains(category)
                );

            if (!query.Any())
                Console.WriteLine("No results");
        }

        // US-022
        public void GetDetails(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductTags)
                    .ThenInclude(pt => pt.Tag)
                .Include(p => p.Reviews)
                .SingleOrDefault(p => p.ProudctId == id);

            var avg = product.Reviews.Average(r => r.Rating);
            var count = product.Reviews.Count();

            Console.WriteLine(avg);
        }

        // US-023
        public void TopProducts()
        {
            var result = _context.Reviews
                .GroupBy(r => new { r.ProductId, r.Product.Name })
                .Select(g => new
                {
                    g.Key.Name,
                    Avg = g.Average(r => r.Rating)
                })
                .OrderByDescending(x => x.Avg)
                .Take(5)
                .ToList();
        }

        // US-024
        public async Task DeactivateOutOfStock()
        {
            await _context.Products
                .Where(p => p.StockQuantity == 0)
                .ExecuteUpdateAsync(p =>
                    p.SetProperty(x => x.IsActive, false)
                );
        }
    }
}
