using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class ProductTag
    {
        public int ProductTagId { get; set; }
        public int ProductId { get; set; } // Foreign Key to Product

        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
