using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages", "shop");
            builder.Property(pr => pr.Url)
                   .IsRequired()
                   .HasMaxLength(500);
            builder.Property(pr => pr.AltText)
                   .HasMaxLength(200);
            builder.Property(pr => pr.IsPrimary)
                   .HasDefaultValue(false);
            builder.HasOne(pr => pr.Product)
                   .WithOne(p => p.ProductImage)
                   .HasForeignKey<ProductImage>(pi => pi.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
