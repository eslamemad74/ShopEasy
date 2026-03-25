using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; } // Foreign Key
        public int CustomerId { get; set; } // Foreign Key
        public int Rating { get; set; } // e.g., 1 to 5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Product Product { get; set; } // Navigation property to Product
        public virtual Customer Customer { get; set; } // Navigation property to Customer
    }
}
