// <copyright file="CompetitionConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(c => c.CompetitionID);

        builder.Property(c => c.DueDate)
            .IsRequired();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Period)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Prompt)
            .IsRequired();

        builder.Property(c => c.TotalVotePoints)
            .IsRequired()
            .HasColumnType("decimal(15,2)");

        builder.HasMany<CompetitionListing>()
            .WithOne()
            .HasForeignKey(cl => cl.CompetitionID);

        builder.HasMany<CompetitionVoteList>()
            .WithOne()
            .HasForeignKey(cvl => cvl.CompeitionListingID);

        builder.HasMany<CompetitionWinnerList>()
            .WithOne()
            .HasForeignKey(cwl => cwl.TopTenPercentListingID);
    }
}
