using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "shop");

            builder.HasKey(p => p.ProudctId);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive)
                   .HasDefaultValue(true);

            builder.Property(p => p.DisplayName)
                   .HasComputedColumnSql("[Name] + ' (' + [SKU] + ')'", true);

            builder.HasQueryFilter(p => p.IsActive);

            builder.HasIndex(p => p.SKU)
                   .IsUnique()
                   .HasDatabaseName("IX_Products_SKU");

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
