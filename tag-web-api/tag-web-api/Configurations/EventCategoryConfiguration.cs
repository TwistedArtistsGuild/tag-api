// <copyright file="EventCategoryConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<EventCategory> builder)
    {
        builder.HasKey(ec => ec.EventCategoryID);

        builder.Property(ec => ec.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ec => ec.CategoryKey)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(ec => ec.CategoryKey)
            .IsUnique();

        builder.Property(ec => ec.Description)
            .HasMaxLength(255);

        builder.Property(ec => ec.Tags)
            .HasMaxLength(255);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<EventCategory> builder)
    {
        builder.HasData(
            new EventCategory { EventCategoryID = 1, Category = "Workshop", CategoryKey = "workshop", Description = "Hands-on learning sessions led by artists or instructors.", Tags = "education, interactive" },
            new EventCategory { EventCategoryID = 2, Category = "Class Series", CategoryKey = "class_series", Description = "Multi-session instructional programs with progressive curriculum.", Tags = "education, recurring" },
            new EventCategory { EventCategoryID = 3, Category = "Live Performance", CategoryKey = "live_performance", Description = "Stage or floor-based performances for a live audience.", Tags = "performance, audience" },
            new EventCategory { EventCategoryID = 4, Category = "Exhibition", CategoryKey = "exhibition", Description = "Curated showcase of visual, digital, or mixed media artwork.", Tags = "gallery, showcase" },
            new EventCategory { EventCategoryID = 5, Category = "Pop-Up Market", CategoryKey = "pop_up_market", Description = "Short-run vendor market featuring artists, makers, and local creatives.", Tags = "market, vendors" },
            new EventCategory { EventCategoryID = 6, Category = "Community Meetup", CategoryKey = "community_meetup", Description = "Casual gatherings for artists, supporters, and creative networking.", Tags = "community, networking" },
            new EventCategory { EventCategoryID = 7, Category = "Competition", CategoryKey = "competition", Description = "Juried or public-voted events where participants compete.", Tags = "contest, juried" },
            new EventCategory { EventCategoryID = 8, Category = "Open Studio", CategoryKey = "open_studio", Description = "Public access sessions where artists share their creative process.", Tags = "studio, behind-the-scenes" },
            new EventCategory { EventCategoryID = 9, Category = "Fundraiser", CategoryKey = "fundraiser", Description = "Events designed to raise financial support for artists or programs.", Tags = "charity, support" },
            new EventCategory { EventCategoryID = 10, Category = "Special Event", CategoryKey = "special_event", Description = "Seasonal, one-off, or collaborative events outside regular programming.", Tags = "seasonal, featured" });
    }
}
