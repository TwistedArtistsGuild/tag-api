// <copyright file="LogConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(log => log.LogID);
            builder.Property(log => log.Critical).HasDefaultValue(false);
            // Use UTC timestamp explicitly for PostgreSQL compatibility
            builder.Property(log => log.LogTimestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(log => log.ShortText).IsRequired();

            // Add other configurations as needed
            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<Log> builder)
        {
            builder.HasData(
                new Log { 
                    LogID = 1, 
                    Critical = true, 
                    LoggedData = "Initial log entry", 
                    ShortText = "Database substantiated", 
                    Tags = "initial,log", 
                    UserID = 1,
                    // Explicitly set LogTimestamp as UTC DateTime for seed data
                    LogTimestamp = new DateTime(2025, 5, 3, 12, 0, 0, DateTimeKind.Utc)
                });
        }
    }
}
