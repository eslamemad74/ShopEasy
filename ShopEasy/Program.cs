using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopEasy.Data;

// Create service collection
var services = new ServiceCollection();

// Load configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Register DbContext
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies());

// Build service provider
var serviceProvider = services.BuildServiceProvider();

// Get DbContext instance (اختياري بس للتجربة)
using (var context = serviceProvider.GetRequiredService<AppDbContext>())
{
    DataSeeder.Seed(context);
    Console.WriteLine("Data Seeded Successfully ");
}