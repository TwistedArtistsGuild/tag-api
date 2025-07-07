// <copyright file="MessageConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.MessageID);
            builder.Property(m => m.Edited).HasDefaultValue(false);
            builder.Property(m => m.Sent).HasDefaultValueSql("NOW()");

            // Add other configurations as needed
        }
    }
}
