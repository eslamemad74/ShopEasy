using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; } 
        public string Status { get;  set; } 
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? ShippedAt { get; set; } 

        public virtual Customer Customer { get; set; } // Navigation property to Customer
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Navigation property to OrderItems
        public virtual Payment Payment { get; set; } // Navigation property to Payment

    }
}
