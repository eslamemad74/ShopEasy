using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; } // Foreign Key to Order
        public string Method { get; set; } // e.g., Credit Card, PayPal
        public string Status { get; set; } // e.g., Completed, Pending, Failed
        public DateTime PaidAt { get; set; }
        public decimal Amount { get; set; }
        public virtual Order Order { get; set; } // Navigation property to Order
    }
}
