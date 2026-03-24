using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopEasy.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public  string  CreatedAt { get; set; }

        // One-to-One
        public CustomerProfile Profile { get; set; }

        // One-to-Many
        public List<Order> Orders { get; set; } = new List<Order>();

        // One-to-Many (Reviews)
        public List<Review> Reviews { get; set; } = new List<Review>();

    }
}
