// <copyright file="BannedListConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class BannedListConfiguration : IEntityTypeConfiguration<BannedList>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BannedList> builder)
    {
        builder.HasKey(bl => bl.BannedListID);

        builder.Property(bl => bl.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(bl => bl.IPAddress)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(bl => bl.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(bl => bl.BannedReason) // Updated to include navigation property
            .WithMany()
            .HasForeignKey(bl => bl.BannedReasonID)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(bl => bl.User) // Updated to include navigation property
            .WithMany()
            .HasForeignKey(bl => bl.UserID);
    }
}
