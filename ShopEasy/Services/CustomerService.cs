using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;

public class CustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    //  US-010 — Register Customer
    public void Register(string name, string email, string phone, string address)
    {
        using var transaction = _context.Database.BeginTransaction();

        var customer = new Customer
        {
            FullName = name,
            Email = email,
            PhoneNumber = phone
        };

        _context.Customers.Add(customer);
        _context.SaveChanges(); // عشان نجيب الـ Id

        var profile = new CustomerProfile
        {
            CustomerId = customer.CustomerId,
            Address = address
        };

        _context.CustomerProfiles.Add(profile);

        _context.SaveChanges();
        transaction.Commit();
    }

    //  US-011 — View Profile
    public void GetProfile(int id)
    {
        var customer = _context.Customers
            .Include(c => c.Profile)
            .Include(c => c.Orders)
            .SingleOrDefault(c => c.CustomerId == id);

        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        Console.WriteLine(customer.FullName);
        Console.WriteLine(customer.Profile?.Address);

        foreach (var order in customer.Orders)
        {
            Console.WriteLine($"Order {order.OrderId} - {order.Status}");
        }
    }

    //  US-012 — Update Address
    public void UpdateAddress(int customerId, string newAddress)
    {
        var customer = _context.Customers
            .Single(c => c.CustomerId == customerId);

        _context.Entry(customer)
            .Reference(c => c.Profile)
            .Load(); // explicit loading

        if (customer.Profile == null)
        {
            customer.Profile = new CustomerProfile
            {
                Address = newAddress,
                CustomerId = customerId
            };
        }
        else
        {
            customer.Profile.Address = newAddress;
        }

        _context.SaveChanges();
    }
}