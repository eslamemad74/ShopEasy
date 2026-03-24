using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Discount
    {
        public int DiscountId { get; set; } 
        public string Code { get; set; } // Unique code for the discount
        public decimal Percentage { get; set; } // Discount percentage (e.g., 10 for 10%)
        public DateTime ExpiresAt { get; set; } // Expiration date of the discount
        public bool IsActive { get; set; } // Indicates if the discount is currently active
        public int MaxUses { get; set; } // Maximum number of times the discount can be used
        public int CurrentUses { get; set; } // Current number of times the discount has been used
    }
}
