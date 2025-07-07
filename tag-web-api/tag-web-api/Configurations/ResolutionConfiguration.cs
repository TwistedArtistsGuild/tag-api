// <copyright file="ResolutionConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class ResolutionConfiguration : IEntityTypeConfiguration<Resolution>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Resolution> builder)
        {
            builder.HasKey(r => r.ResolutionID);

            builder.Property(r => r.Body)
                .HasColumnType("text");

            builder.Property(r => r.CanceledDate)
                .HasColumnType("timestamptz");

            builder.Property(r => r.CanceledReason)
                .HasColumnType("text");

            builder.Property(r => r.DueDate)
                .HasColumnType("timestamptz");

            builder.Property(r => r.Executed)
                .IsRequired();

            builder.Property(r => r.ExecutedDate)
                .HasColumnType("timestamptz");

            builder.Property(r => r.MultipleChoice)
                .IsRequired();

            builder.Property(r => r.Subject)
                .HasColumnType("text");

            builder.Property(r => r.Timestamp)
                .HasColumnType("timestamptz");

            builder.Property(r => r.Title)
                .HasMaxLength(1000);
        }
    }
}
