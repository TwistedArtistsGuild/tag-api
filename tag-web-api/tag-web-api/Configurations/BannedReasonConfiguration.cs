// <copyright file="BannedReasonConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class BannedReasonConfiguration : IEntityTypeConfiguration<BannedReason>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BannedReason> builder)
    {
        builder.HasKey(br => br.BannedReasonID);

        builder.Property(br => br.BannedOn)
            .IsRequired();

        builder.Property(br => br.PublicReasonForBan)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(br => br.ReasonForBan)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(br => br.SupportingDocsURL)
            .HasMaxLength(255);
    }
}
