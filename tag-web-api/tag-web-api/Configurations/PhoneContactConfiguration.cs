// <copyright file="PhoneContactConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class PhoneContactConfiguration : IEntityTypeConfiguration<PhoneContact>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PhoneContact> builder)
        {
            builder.HasKey(pc => pc.PhoneContactID);

            builder.Property(pc => pc.PhoneNumber)
                .IsRequired();

            builder.Property(pc => pc.Description)
                .HasMaxLength(255);

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<PhoneContact> builder)
        {
            builder.HasData(
                new PhoneContact { PhoneContactID = 1, PhoneNumber = "404-123-4567", Description = "Atlanta Office" },
                new PhoneContact { PhoneContactID = 2, PhoneNumber = "912-234-5678", Description = "Savannah Office" },
                new PhoneContact { PhoneContactID = 3, PhoneNumber = "706-345-6789", Description = "Augusta Office" },
                new PhoneContact { PhoneContactID = 4, PhoneNumber = "704-456-7890", Description = "Charlotte Office" },
                new PhoneContact { PhoneContactID = 5, PhoneNumber = "919-567-8901", Description = "Raleigh Office" },
                new PhoneContact { PhoneContactID = 6, PhoneNumber = "336-678-9012", Description = "Greensboro Office" },
                new PhoneContact { PhoneContactID = 7, PhoneNumber = "843-789-0123", Description = "Charleston Office" },
                new PhoneContact { PhoneContactID = 8, PhoneNumber = "803-890-1234", Description = "Columbia Office" },
                new PhoneContact { PhoneContactID = 9, PhoneNumber = "864-901-2345", Description = "Greenville Office" }
            );
        }
    }
}
