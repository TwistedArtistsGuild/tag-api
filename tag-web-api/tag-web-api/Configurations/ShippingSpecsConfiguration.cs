// <copyright file="ShippingSpecsConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class ShippingSpecsConfiguration : IEntityTypeConfiguration<ShippingSpecs>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ShippingSpecs> builder)
        {
            builder.HasKey(s => s.ShippingSpecsID);

            builder.Property(s => s.CarrierAccount)
                .HasMaxLength(255);

            builder.Property(s => s.DistanceUnit)
                .HasMaxLength(25);

            builder.Property(s => s.HazardousShipping)
                .IsRequired();

            builder.Property(s => s.Height)
                .HasColumnType("decimal(15,2)");

            builder.Property(s => s.MassUnit)
                .HasMaxLength(25);

            builder.Property(s => s.PackageType)
                .HasColumnType("text");

            builder.Property(s => s.ShippingNotes)
                .HasColumnType("text");

            builder.Property(s => s.ShippingWeight)
                .HasColumnType("decimal(15,2)");

            builder.Property(s => s.ShippoObjectID)
                .HasMaxLength(255);

            builder.Property(s => s.ShipWeight)
                .HasColumnType("decimal(15,2)");

            builder.Property(s => s.Units)
                .HasMaxLength(100);

            builder.Property(s => s.Weight)
                .HasColumnType("decimal(15,2)");
        }
    }
}
