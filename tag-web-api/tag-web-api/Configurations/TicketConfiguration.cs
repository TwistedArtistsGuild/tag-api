// <copyright file="TicketConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.TicketID);

            builder.Property(t => t.AttendeeName)
                .HasColumnType("text");

            builder.Property(t => t.SoldTimestamp)
                .HasColumnType("timestamptz");

            builder.Property(t => t.TicketTypeID)
                .IsRequired();

            builder.Property(t => t.UniqueHash)
                .HasMaxLength(255);

            builder.HasOne<TicketType>()
                .WithMany()
                .HasForeignKey(t => t.TicketTypeID);
        }
    }
}
