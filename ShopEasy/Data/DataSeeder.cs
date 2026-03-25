using ShopEasy.Data;
using ShopEasy.Models;
using System.Text.Json;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Customers.Any())
            return; // already seeded

        using var transaction = context.Database.BeginTransaction();

        try
        {
            var customers = LoadJson<List<Customer>>("JsonData/customers.json");
            var categories = LoadJson<List<Category>>("JsonData/categories.json");
            var products = LoadJson<List<Product>>("JsonData/products.json");
            var tags = LoadJson<List<Tag>>("JsonData/tags.json");
            var productTags =Path.Combine("JsonData", "productTags.json");              //LoadJson < List < ProductTag >>("JsonData/productTags.json");
            var orders = LoadJson<List<Order>>("JsonData/orders.json");
            var orderItems = LoadJson<List<OrderItem>>("JsonData/orderItems.json");
            var reviews = LoadJson<List<Review>>("JsonData/reviews.json");
            var payments = Path.Combine("JsonData", "Payment.json");                                    //LoadJson<List<Payment>>("JsonData/payments.json");
            var discounts = LoadJson<List<Discount>>("JsonData/discounts.json");



            context.AddRange(customers);
            context.AddRange(categories);
            context.AddRange(products);
            context.AddRange(tags);
            context.AddRange(productTags);
            context.AddRange(orders);
            context.AddRange(orderItems);
            context.AddRange(reviews);
            context.AddRange(payments);
            context.AddRange(discounts);
            
            context.SaveChanges();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    
    private static T LoadJson<T>(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json);
    }
    

    /*
    private static T LoadJson<T>(string path)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

        var json = File.ReadAllText(fullPath);

        Console.WriteLine($"Reading: {path}");
        Console.WriteLine(json); 

        return JsonSerializer.Deserialize<T>(json)!;
    }
    */

    /*
    private static T LoadJson<T>(string path)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

        var json = File.ReadAllText(fullPath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true 
        };

        return JsonSerializer.Deserialize<T>(json, options)!;
    }

    */
}