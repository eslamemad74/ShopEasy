using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopEasy.Data;
using ShopEasy.Services;

// Create service collection
var serviceCollection = new ServiceCollection();

// Load configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Register DbContext
serviceCollection.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies());

// Register Services
serviceCollection.AddScoped<ProductService>();
serviceCollection.AddScoped<OrderService>();
serviceCollection.AddScoped<CustomerService>();
serviceCollection.AddScoped<DiscountService>();

// Build service provider
var serviceProvider = serviceCollection.BuildServiceProvider();

// Create scope
using (var scope = serviceProvider.CreateScope())
{
    var provider = scope.ServiceProvider;

    // Seed Data
    var context = provider.GetRequiredService<AppDbContext>();
    DataSeeder.Seed(context);
    Console.WriteLine("Data Seeded Successfully ✅");

    // Use Services
    var productService = provider.GetRequiredService<ProductService>();
    var orderService = provider.GetRequiredService<OrderService>();
    var customerService = provider.GetRequiredService<CustomerService>();

    // Test calls
    productService.GetAllProducts();
    productService.TopProducts();

   
}