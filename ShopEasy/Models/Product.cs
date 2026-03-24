using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Product
    {
        public int ProudctId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int SKU { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }

        // FK
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        // One-to-Many with OrderItems
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Many-to-Many with Tags
        public List<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        // One-to-One with ProductImage
        public ProductImage ProductImage { get; set; }

        // Reviews
        public List<Review> Reviews { get; set; } = new List<Review>();

        // Computed Column (هنعمله في Configuration)
        public string DisplayName { get; set; }
    }
}
