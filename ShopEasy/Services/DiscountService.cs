using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;

public class DiscountService
{
    private readonly AppDbContext _context;

    public DiscountService(AppDbContext context)
    {
        _context = context;
    }

    // US-040
    public void ApplyDiscount(Order order, string code)
    {
        var discount = _context.Discounts
            .SingleOrDefault(d => d.Code == code);

        if (discount != null &&
            discount.IsActive &&
            discount.ExpiresAt > DateTime.UtcNow &&
            discount.CurrentUses < discount.MaxUses)
        {
            order.TotalAmount -= order.TotalAmount * (discount.Percentage / 100);
            discount.CurrentUses++;
        }
    }

    // US-041
    public async Task DeleteExpired()
    {
        await _context.Discounts
            .Where(d => d.ExpiresAt < DateTime.UtcNow || !d.IsActive)
            .ExecuteDeleteAsync();
    }
}