using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class ProductTag
    {
        public int ProductTagId { get; set; }
        public int ProductId { get; set; } // Foreign Key to Product

        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
