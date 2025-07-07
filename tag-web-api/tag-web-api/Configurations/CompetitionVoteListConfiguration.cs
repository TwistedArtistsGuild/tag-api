// <copyright file="CompetitionVoteListConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class CompetitionVoteListConfiguration : IEntityTypeConfiguration<CompetitionVoteList>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CompetitionVoteList> builder)
    {
        builder.HasKey(cvl => cvl.CompetitionVoteListID);

        builder.Property(cvl => cvl.Comments)
            .HasColumnType("text");

        builder.Property(cvl => cvl.Timestamp)
            .IsRequired();

        builder.HasOne<CompetitionListing>()
            .WithMany()
            .HasForeignKey(cvl => cvl.CompeitionListingID);
    }
}
