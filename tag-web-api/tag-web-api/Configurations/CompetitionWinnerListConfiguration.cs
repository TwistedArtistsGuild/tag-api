// <copyright file="CompetitionWinnerListConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class CompetitionWinnerListConfiguration : IEntityTypeConfiguration<CompetitionWinnerList>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CompetitionWinnerList> builder)
    {
        builder.HasKey(cwl => cwl.CompetitionWinnerListID);

        builder.Property(cwl => cwl.Place)
            .IsRequired();

        builder.Property(cwl => cwl.VotesForListing)
            .IsRequired();

        builder.HasOne<CompetitionListing>()
            .WithMany()
            .HasForeignKey(cwl => cwl.TopTenPercentListingID);
    }
}
