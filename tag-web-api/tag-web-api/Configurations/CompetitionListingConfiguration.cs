// <copyright file="CompetitionListingConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class CompetitionListingConfiguration : IEntityTypeConfiguration<CompetitionListing>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CompetitionListing> builder)
    {
        builder.HasKey(cl => cl.CompetitionListingID);

        builder.HasOne<Competition>()
            .WithMany()
            .HasForeignKey(cl => cl.CompetitionID);

        builder.HasOne<Listing>()
            .WithMany()
            .HasForeignKey(cl => cl.ListingID);
    }
}
