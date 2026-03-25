using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int productId { get; set; } // Foreign Key to Product
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; } // Navigation property to Order
        public virtual Product Product { get; set; } // Navigation property to Product
    }
}
