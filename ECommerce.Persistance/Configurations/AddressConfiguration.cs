﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECommerce.Domain.Entities;

namespace ECommerce.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AddressLine1)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.AddressLine2)
                .HasMaxLength(200);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
