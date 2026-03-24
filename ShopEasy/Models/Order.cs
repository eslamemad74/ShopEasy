using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; } // Foreign Key to Customer
        public string Status { get; set; } // e.g., "Pending", "Shipped", "Delivered"
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? ShippedAt { get; set; } // Nullable, as it may not be shipped yet

        public  Customer Customer { get; set; } // Navigation property to Customer
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Navigation property to OrderItems
        public Payment Payment { get; set; } // Navigation property to Payment

    }
}
