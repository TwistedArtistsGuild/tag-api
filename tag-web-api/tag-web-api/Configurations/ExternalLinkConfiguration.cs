// <copyright file="ExternalLinkConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class ExternalLinkConfiguration : IEntityTypeConfiguration<ExternalLink>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ExternalLink> builder)
        {
            builder.HasKey(el => el.ExternalLinkID);

            builder.Property(el => el.Description)
                .HasMaxLength(255);

            builder.Property(el => el.Handle)
                .HasMaxLength(100);

            builder.Property(el => el.ServiceName)
                .HasMaxLength(100);

            builder.Property(el => el.URL)
                .IsRequired()
                .HasMaxLength(255);

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<ExternalLink> builder)
        {
            builder.HasData(
                new ExternalLink { ExternalLinkID = 1, URL = "https://www.reddit.com/user/atlanta_office", Description = "Atlanta Office Reddit", Handle = "atlanta_office", ServiceName = "Reddit" },
                new ExternalLink { ExternalLinkID = 2, URL = "https://www.facebook.com/savannah_office", Description = "Savannah Office Facebook", Handle = "savannah_office", ServiceName = "Facebook" },
                new ExternalLink { ExternalLinkID = 3, URL = "https://www.instagram.com/augusta_office", Description = "Augusta Office Instagram", Handle = "augusta_office", ServiceName = "Instagram" },
                new ExternalLink { ExternalLinkID = 4, URL = "https://www.reddit.com/user/charlotte_office", Description = "Charlotte Office Reddit", Handle = "charlotte_office", ServiceName = "Reddit" },
                new ExternalLink { ExternalLinkID = 5, URL = "https://satarahpresents.com/", Description = "Studio Satarah Official Website", Handle = "satarahpresents", ServiceName = "Website" },
                new ExternalLink { ExternalLinkID = 6, URL = "https://www.instagram.com/greensboro_office", Description = "Greensboro Office Instagram", Handle = "greensboro_office", ServiceName = "Instagram" },
                new ExternalLink { ExternalLinkID = 7, URL = "https://www.reddit.com/user/charleston_office", Description = "Charleston Office Reddit", Handle = "charleston_office", ServiceName = "Reddit" },
                new ExternalLink { ExternalLinkID = 8, URL = "https://www.facebook.com/columbia_office", Description = "Columbia Office Facebook", Handle = "columbia_office", ServiceName = "Facebook" },
                new ExternalLink { ExternalLinkID = 9, URL = "https://www.instagram.com/greenville_office", Description = "Greenville Office Instagram", Handle = "greenville_office", ServiceName = "Instagram" }
            );
        }
    }
}
