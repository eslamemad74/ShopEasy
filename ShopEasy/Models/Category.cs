using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; } // Self-referencing foreign key
        public virtual  Category ParentCategory { get; set; }

        public virtual List<Category> SubCategories { get; set; } = new List<Category>();
        // One-to-Many with Products
        public virtual List<Product> Products { get; set; } = new List<Product>();

        // مش هنستخدمه في DB (هنتجاهله بعدين في Configuration)
        public string InternalNotes { get; set; }
    }
}
