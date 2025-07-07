// <copyright file="DigitalDeliverySpecsConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class DigitalDeliverySpecsConfiguration : IEntityTypeConfiguration<DigitalDeliverySpecs>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DigitalDeliverySpecs> builder)
    {
        builder.HasKey(dds => dds.DigitalDeliverySpecsID);

        builder.Property(dds => dds.DeliveryURL)
            .HasMaxLength(255);

        builder.Property(dds => dds.Duration)
            .IsRequired();

        builder.Property(dds => dds.LeadTime)
            .HasMaxLength(255);

        builder.Property(dds => dds.PromiseDescription)
            .HasColumnType("text");
    }
}
