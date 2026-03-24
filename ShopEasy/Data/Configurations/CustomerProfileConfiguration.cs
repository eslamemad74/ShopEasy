using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Data.Configurations
{

    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
          builder.ToTable("CustomerProfiles","Shop");
          builder.Property(cp => cp.Address)
                 .HasMaxLength(200);
          builder.Property(cp => cp.City)
                 .HasMaxLength(100);
          builder.Property(cp => cp.PostalCode)
                 .HasMaxLength(20);
          builder.Property(cp => cp.NationalId)
                 .HasMaxLength(50)
                 .HasColumnType("nchar(50)");
          builder.HasOne(cp => cp.Customer)
                 .WithOne(c => c.Profile)
                 .HasForeignKey<CustomerProfile>(cp => cp.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
