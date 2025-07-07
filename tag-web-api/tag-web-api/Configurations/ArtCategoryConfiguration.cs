// <copyright file="ArtCategoryConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class ArtCategoryConfiguration : IEntityTypeConfiguration<ArtCategory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ArtCategory> builder)
    {
        builder.HasKey(ac => ac.ArtCategoryID);

        builder.Property(ac => ac.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ac => ac.CategoryKey)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(ac => ac.CategoryKey)
            .IsUnique();

        builder.Property(ac => ac.Description)
            .HasMaxLength(255);

        builder.Property(ac => ac.Tags)
            .HasMaxLength(255);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<ArtCategory> builder)
    {
        builder.HasData(
            new ArtCategory { ArtCategoryID = 1, Category = "Painting", CategoryKey = "painting", Description = "An expressive medium using color, brush, and canvas to capture emotion and imagery.", Tags = "physicalart, painting" },
            new ArtCategory { ArtCategoryID = 2, Category = "Sculpture", CategoryKey = "sculpture", Description = "A three-dimensional art form that shapes materials like clay, stone, or metal into enduring forms.", Tags = "physicalart, sculpture" },
            new ArtCategory { ArtCategoryID = 3, Category = "Tie Dye", CategoryKey = "tie_dye", Description = "A vibrant textile art form that employs dye techniques to create unique patterns on fabric.", Tags = "physicalart, tiedye" },
            new ArtCategory { ArtCategoryID = 4, Category = "Fiber Arts", CategoryKey = "fiber_arts", Description = "An intricate craft that uses fibers, yarns, and textiles to produce tactile, woven artworks.", Tags = "physicalart, fiberarts" },
            new ArtCategory { ArtCategoryID = 5, Category = "Sketch", CategoryKey = "sketch", Description = "A swift and expressive drawing that captures the essence of a subject using minimal lines.", Tags = "physicalart, sketch" },
            new ArtCategory { ArtCategoryID = 6, Category = "Drawing", CategoryKey = "drawing", Description = "A detailed and meticulous rendering using pen, ink, or charcoal to express form and emotion.", Tags = "physicalart, drawing" },
            new ArtCategory { ArtCategoryID = 7, Category = "Digital Art", CategoryKey = "digital_art", Description = "Art created through digital tools and software, merging technology with creative expression.", Tags = "digitalart, digitalart" },
            new ArtCategory { ArtCategoryID = 8, Category = "Website Creation", CategoryKey = "website_creation", Description = "The craft of designing and building engaging, interactive digital spaces using code and design principles.", Tags = "digitalart, websitecreation" },
            new ArtCategory { ArtCategoryID = 9, Category = "Dance", CategoryKey = "dance", Description = "A dynamic performance emphasizing movement, rhythm, and expression through choreographed motion.", Tags = "performanceart, dance" },
            new ArtCategory { ArtCategoryID = 10, Category = "Circus Arts", CategoryKey = "circus_arts", Description = "A thrilling live performance that blends acrobatics, juggling, and theatrical artistry.", Tags = "performanceart, circusarts" },
            new ArtCategory { ArtCategoryID = 11, Category = "Band", CategoryKey = "band", Description = "A musical performance that unites instruments and vocals to create an immersive live experience.", Tags = "performanceart, band" },
            new ArtCategory { ArtCategoryID = 12, Category = "Disk Jockey (DJ)", CategoryKey = "disk_jockey", Description = "An innovative performance where curated music is mixed live using turntables and other tools.", Tags = "performanceart, dj" },
            new ArtCategory { ArtCategoryID = 13, Category = "Master of Ceremony (MC)", CategoryKey = "master_of_ceremony", Description = "A live host and entertainer, engaging the audience and guiding events with charisma.", Tags = "performanceart, mc" },
            new ArtCategory { ArtCategoryID = 14, Category = "Music Producers", CategoryKey = "music_producers", Description = "Artists creating music by mixing, composing, or arranging audio. This form is not currently supported.", Tags = "digitalart, musicproducers" },
            new ArtCategory { ArtCategoryID = 15, Category = "Photography", CategoryKey = "photography", Description = "The art of capturing and preserving moments through compelling composition and effective lighting.", Tags = "physicalart, photography" },
            new ArtCategory { ArtCategoryID = 16, Category = "Printmaking", CategoryKey = "printmaking", Description = "A traditional art form employing carving, etching, or screen printing techniques to produce reproducible images.", Tags = "physicalart, printmaking" },
            new ArtCategory { ArtCategoryID = 17, Category = "Mixed Media", CategoryKey = "mixed_media", Description = "Artwork that combines a variety of materials and techniques, resulting in layered, textured expressions.", Tags = "physicalart, mixedmedia" },
            new ArtCategory { ArtCategoryID = 18, Category = "Installation Art", CategoryKey = "installation_art", Description = "Large-scale and immersive art installations designed to transform and engage physical spaces.", Tags = "physicalart, installationart" },
            new ArtCategory { ArtCategoryID = 19, Category = "Ceramics", CategoryKey = "ceramics", Description = "The craft of molding, firing, and glazing clay to produce both functional and decorative art objects.", Tags = "physicalart, ceramics" },
            new ArtCategory { ArtCategoryID = 20, Category = "Collage", CategoryKey = "collage", Description = "The creative assembly of various materials to form a unified, imaginative artwork.", Tags = "physicalart, collage" },
            new ArtCategory { ArtCategoryID = 21, Category = "Mosaic", CategoryKey = "mosaic", Description = "A detailed art form that assembles small pieces of tile, glass, or stone into intricate designs.", Tags = "physicalart, mosaic" },
            new ArtCategory { ArtCategoryID = 22, Category = "Street Art", CategoryKey = "street_art", Description = "Urban creativity expressed through murals, graffiti, and public installations that transform cityscapes.", Tags = "physicalart, streetart" },
            new ArtCategory { ArtCategoryID = 23, Category = "Conceptual Art", CategoryKey = "conceptual_art", Description = "An art form in which the concept or idea behind the work is more significant than its physical execution.", Tags = "physicalart, conceptualart" },
            new ArtCategory { ArtCategoryID = 24, Category = "Animation", CategoryKey = "animation", Description = "The process of creating moving images through sequential frames, blending artistic storytelling with technology.", Tags = "digitalart, animation" },
            new ArtCategory { ArtCategoryID = 25, Category = "Architecture", CategoryKey = "architecture", Description = "The creative discipline of designing and constructing functional, aesthetically pleasing physical spaces.", Tags = "physicalart, architecture" },
            new ArtCategory { ArtCategoryID = 26, Category = "Calligraphy", CategoryKey = "calligraphy", Description = "The art of beautiful handwriting, focusing on the elegance, form, and fluidity of written script.", Tags = "physicalart, calligraphy" },
            new ArtCategory { ArtCategoryID = 27, Category = "Fashion Design", CategoryKey = "fashion_design", Description = "A creative endeavor that merges aesthetic vision with practical design to produce wearable art.", Tags = "physicalart, fashiondesign" },
            new ArtCategory { ArtCategoryID = 28, Category = "Video Art", CategoryKey = "video_art", Description = "A contemporary medium that uses video technology to explore narrative structures and abstract concepts.", Tags = "digitalart, videoart" },
            new ArtCategory { ArtCategoryID = 29, Category = "Interactive Art", CategoryKey = "interactive_art", Description = "Innovative art that actively engages the audience through digital interfaces and immersive experiences.", Tags = "digitalart, interactiveart" },
            new ArtCategory { ArtCategoryID = 30, Category = "Merchandise", CategoryKey = "merchandise", Description = "Creative products designed for sale, often featuring artistic designs or branding.", Tags = "physicalart, merchandise" });
    }
}
