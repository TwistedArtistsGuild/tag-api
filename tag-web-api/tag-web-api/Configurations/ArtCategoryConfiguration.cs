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

        builder.HasOne(ac => ac.ParentCategory)
            .WithMany(ac => ac.SubCategories)
            .HasForeignKey(ac => ac.ParentArtCategoryID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<ArtCategory> builder)
    {
        builder.HasData(
            // Level 1: Root Categories (3 items)
            new ArtCategory { ArtCategoryID = 1, Category = "Physical Art", CategoryKey = "physical_art", Description = "Tangible art forms created through hands-on techniques using traditional materials and mediums.", Tags = "root, physicalart", ParentArtCategoryID = null },
            new ArtCategory { ArtCategoryID = 2, Category = "Digital Art", CategoryKey = "digital_art", Description = "Art created through digital tools, software, and technology, representing modern creative expression.", Tags = "root, digitalart", ParentArtCategoryID = null },
            new ArtCategory { ArtCategoryID = 3, Category = "Event-Based Art", CategoryKey = "event_based_art", Description = "Live performances and interactive experiences shared with audiences in real-time.", Tags = "root, performanceart", ParentArtCategoryID = null },

            // Level 2 under Physical Art (IDs 4-10)
            new ArtCategory { ArtCategoryID = 4, Category = "Painting & Drawing", CategoryKey = "painting_drawing", Description = "Two-dimensional art using paint, pencil, ink, and other drawing media.", Tags = "physicalart, painting", ParentArtCategoryID = 1 },
            new ArtCategory { ArtCategoryID = 5, Category = "Sculpture & 3D Forms", CategoryKey = "sculpture_3d", Description = "Three-dimensional art forms created by shaping materials like clay, stone, or metal.", Tags = "physicalart, sculpture", ParentArtCategoryID = 1 },
            new ArtCategory { ArtCategoryID = 6, Category = "Fiber & Textile Arts", CategoryKey = "fiber_textile", Description = "Art created through fibers, yarns, fabrics, and textile techniques.", Tags = "physicalart, fiberarts", ParentArtCategoryID = 1 },
            new ArtCategory { ArtCategoryID = 7, Category = "Photography & Printmaking", CategoryKey = "photography_print", Description = "Capturing and reproducing images through photography, etching, and printing techniques.", Tags = "physicalart, photography", ParentArtCategoryID = 1 },
            new ArtCategory { ArtCategoryID = 8, Category = "Mixed Media & Crafts", CategoryKey = "mixed_media_crafts", Description = "Artwork combining multiple materials and techniques, including ceramics and decorative arts.", Tags = "physicalart, mixedmedia", ParentArtCategoryID = 1 },
            new ArtCategory { ArtCategoryID = 9, Category = "Street & Installation Art", CategoryKey = "street_installation", Description = "Large-scale and immersive artworks in public spaces and urban environments.", Tags = "physicalart, streetart", ParentArtCategoryID = 1 },
            new ArtCategory { ArtCategoryID = 10, Category = "Architecture & Design", CategoryKey = "architecture_design", Description = "Design and construction of functional and aesthetic physical structures and spaces.", Tags = "physicalart, architecture", ParentArtCategoryID = 1 },

            // Level 2 under Digital Art (IDs 11-15)
            new ArtCategory { ArtCategoryID = 11, Category = "Visual & Graphic Design", CategoryKey = "visual_graphic_design", Description = "Digital design for visual communication, branding, and conceptual expression.", Tags = "digitalart, design", ParentArtCategoryID = 2 },
            new ArtCategory { ArtCategoryID = 12, Category = "Animation & Video Art", CategoryKey = "animation_video", Description = "Moving imagery created through sequential frames, digital storytelling, and video techniques.", Tags = "digitalart, animation", ParentArtCategoryID = 2 },
            new ArtCategory { ArtCategoryID = 13, Category = "Music & Audio Production", CategoryKey = "music_audio", Description = "Creation and production of music, sound design, and audio as a digital medium.", Tags = "digitalart, musicproduction", ParentArtCategoryID = 2 },
            new ArtCategory { ArtCategoryID = 14, Category = "Interactive & Web Art", CategoryKey = "interactive_web", Description = "Digital art that engages audiences through interactive experiences and web technologies.", Tags = "digitalart, interactive", ParentArtCategoryID = 2 },
            new ArtCategory { ArtCategoryID = 15, Category = "Software & Creative Code", CategoryKey = "software_creative_code", Description = "Art created through code, web design, and digital platforms.", Tags = "digitalart, software", ParentArtCategoryID = 2 },

            // Level 2 under Event-Based Art (IDs 16-19)
            new ArtCategory { ArtCategoryID = 16, Category = "Dance & Movement", CategoryKey = "dance_movement", Description = "Choreographed and improvisational movement expressing emotion and artistry.", Tags = "performanceart, dance", ParentArtCategoryID = 3 },
            new ArtCategory { ArtCategoryID = 17, Category = "Circus & Acrobatics", CategoryKey = "circus_acrobatics", Description = "Thrilling performances blending acrobatics, juggling, and theatrical elements.", Tags = "performanceart, circusarts", ParentArtCategoryID = 3 },
            new ArtCategory { ArtCategoryID = 18, Category = "Live Music Performance", CategoryKey = "live_music", Description = "Live musical performances including bands, ensembles, and musical entertainment.", Tags = "performanceart, music", ParentArtCategoryID = 3 },
            new ArtCategory { ArtCategoryID = 19, Category = "Spoken Word & Hosting", CategoryKey = "spoken_word_hosting", Description = "Live spoken performance including comedy, poetry, spoken word, and event hosting.", Tags = "performanceart, spoken", ParentArtCategoryID = 3 },

            // Level 3 under Painting & Drawing (ID 4)
            new ArtCategory { ArtCategoryID = 20, Category = "Painting", CategoryKey = "painting", Description = "An expressive medium using color, brush, and canvas to capture emotion and imagery.", Tags = "painting", ParentArtCategoryID = 4 },
            new ArtCategory { ArtCategoryID = 21, Category = "Drawing", CategoryKey = "drawing", Description = "A detailed and meticulous rendering using pen, ink, or charcoal to express form and emotion.", Tags = "drawing", ParentArtCategoryID = 4 },
            new ArtCategory { ArtCategoryID = 22, Category = "Sketch", CategoryKey = "sketch", Description = "A swift and expressive drawing that captures the essence of a subject using minimal lines.", Tags = "sketch", ParentArtCategoryID = 4 },
            new ArtCategory { ArtCategoryID = 23, Category = "Calligraphy", CategoryKey = "calligraphy", Description = "The art of beautiful handwriting, focusing on the elegance, form, and fluidity of written script.", Tags = "calligraphy", ParentArtCategoryID = 4 },

            // Level 3 under Sculpture & 3D Forms (ID 5)
            new ArtCategory { ArtCategoryID = 24, Category = "Sculpture", CategoryKey = "sculpture", Description = "A three-dimensional art form that shapes materials like clay, stone, or metal into enduring forms.", Tags = "sculpture", ParentArtCategoryID = 5 },
            new ArtCategory { ArtCategoryID = 25, Category = "Ceramics", CategoryKey = "ceramics", Description = "The craft of molding, firing, and glazing clay to produce both functional and decorative art objects.", Tags = "ceramics", ParentArtCategoryID = 5 },

            // Level 3 under Fiber & Textile Arts (ID 6)
            new ArtCategory { ArtCategoryID = 26, Category = "Tie Dye", CategoryKey = "tie_dye", Description = "A vibrant textile art form that employs dye techniques to create unique patterns on fabric.", Tags = "tiedye", ParentArtCategoryID = 6 },
            new ArtCategory { ArtCategoryID = 27, Category = "Fiber Arts", CategoryKey = "fiber_arts", Description = "An intricate craft that uses fibers, yarns, and textiles to produce tactile, woven artworks.", Tags = "fiberarts", ParentArtCategoryID = 6 },

            // Level 3 under Photography & Printmaking (ID 7)
            new ArtCategory { ArtCategoryID = 28, Category = "Photography", CategoryKey = "photography", Description = "The art of capturing and preserving moments through compelling composition and effective lighting.", Tags = "photography", ParentArtCategoryID = 7 },
            new ArtCategory { ArtCategoryID = 29, Category = "Printmaking", CategoryKey = "printmaking", Description = "A traditional art form employing carving, etching, or screen printing techniques to produce reproducible images.", Tags = "printmaking", ParentArtCategoryID = 7 },

            // Level 3 under Mixed Media & Crafts (ID 8)
            new ArtCategory { ArtCategoryID = 30, Category = "Mixed Media", CategoryKey = "mixed_media", Description = "Artwork that combines a variety of materials and techniques, resulting in layered, textured expressions.", Tags = "mixedmedia", ParentArtCategoryID = 8 },
            new ArtCategory { ArtCategoryID = 31, Category = "Collage", CategoryKey = "collage", Description = "The creative assembly of various materials to form a unified, imaginative artwork.", Tags = "collage", ParentArtCategoryID = 8 },
            new ArtCategory { ArtCategoryID = 32, Category = "Mosaic", CategoryKey = "mosaic", Description = "A detailed art form that assembles small pieces of tile, glass, or stone into intricate designs.", Tags = "mosaic", ParentArtCategoryID = 8 },
            new ArtCategory { ArtCategoryID = 33, Category = "Fashion Design", CategoryKey = "fashion_design", Description = "A creative endeavor that merges aesthetic vision with practical design to produce wearable art.", Tags = "fashiondesign", ParentArtCategoryID = 8 },
            new ArtCategory { ArtCategoryID = 34, Category = "Merchandise", CategoryKey = "merchandise", Description = "Creative products designed for sale, often featuring artistic designs or branding.", Tags = "merchandise", ParentArtCategoryID = 8 },

            // Level 3 under Street & Installation Art (ID 9)
            new ArtCategory { ArtCategoryID = 35, Category = "Street Art", CategoryKey = "street_art", Description = "Urban creativity expressed through murals, graffiti, and public installations that transform cityscapes.", Tags = "streetart", ParentArtCategoryID = 9 },
            new ArtCategory { ArtCategoryID = 36, Category = "Installation Art", CategoryKey = "installation_art", Description = "Large-scale and immersive art installations designed to transform and engage physical spaces.", Tags = "installationart", ParentArtCategoryID = 9 },
            new ArtCategory { ArtCategoryID = 37, Category = "Conceptual Art", CategoryKey = "conceptual_art", Description = "An art form in which the concept or idea behind the work is more significant than its physical execution.", Tags = "conceptualart", ParentArtCategoryID = 9 },

            // Level 3 under Visual & Graphic Design (ID 11)
            new ArtCategory { ArtCategoryID = 38, Category = "Graphic Design", CategoryKey = "graphic_design", Description = "Visual design for communication, branding, and digital platforms.", Tags = "graphicdesign", ParentArtCategoryID = 11 },
            new ArtCategory { ArtCategoryID = 39, Category = "Conceptual Design", CategoryKey = "conceptual_design", Description = "Innovative design exploring ideas and pushing creative boundaries.", Tags = "conceptualdesign", ParentArtCategoryID = 11 },

            // Level 3 under Animation & Video Art (ID 12)
            new ArtCategory { ArtCategoryID = 40, Category = "Animation", CategoryKey = "animation", Description = "The process of creating moving images through sequential frames, blending artistic storytelling with technology.", Tags = "animation", ParentArtCategoryID = 12 },
            new ArtCategory { ArtCategoryID = 41, Category = "Video Art", CategoryKey = "video_art", Description = "A contemporary medium that uses video technology to explore narrative structures and abstract concepts.", Tags = "videoart", ParentArtCategoryID = 12 },

            // Level 3 under Music & Audio Production (ID 13)
            new ArtCategory { ArtCategoryID = 42, Category = "Music Production", CategoryKey = "music_production", Description = "The creation and production of music through composition, mixing, and arrangement.", Tags = "musicproduction", ParentArtCategoryID = 13 },

            // Level 3 under Interactive & Web Art (ID 14)
            new ArtCategory { ArtCategoryID = 44, Category = "Interactive Art", CategoryKey = "interactive_art", Description = "Innovative art that actively engages the audience through digital interfaces and immersive experiences.", Tags = "interactiveart", ParentArtCategoryID = 14 },
            new ArtCategory { ArtCategoryID = 45, Category = "Web Art", CategoryKey = "web_art", Description = "Art created specifically for the web using HTML, CSS, and interactive technologies.", Tags = "webdesign", ParentArtCategoryID = 14 },

            // Level 3 under Software & Creative Code (ID 15)
            new ArtCategory { ArtCategoryID = 46, Category = "Website Creation", CategoryKey = "website_creation", Description = "The craft of designing and building engaging, interactive digital spaces using code and design principles.", Tags = "websitecreation", ParentArtCategoryID = 15 });
    }
}
