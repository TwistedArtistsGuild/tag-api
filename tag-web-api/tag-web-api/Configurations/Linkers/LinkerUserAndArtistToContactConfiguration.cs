// <copyright file="LinkerUserAndArtistToContactConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    /// <summary>
    /// Configuration class for the Linker_UserAndArtistToContact entity.
    /// This class configures the many-to-many relationships between artists/users and various contact types.
    /// </summary>
    public class LinkerUserAndArtistToContactConfiguration : IEntityTypeConfiguration<Linker_UserAndArtistToContact>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Linker_UserAndArtistToContact> builder)
        {
            // Primary key
            builder.HasKey(l => l.Linker_UserAndArtistToContactID);

            // Properties
            builder.Property(l => l.Label)
                .HasMaxLength(255);

            builder.Property(l => l.MakePublic)
                .HasDefaultValue(false);
                
            // Relationships - set up each relationship with proper cascading behavior
            // Artist relationship
            builder.HasOne(l => l.Artist)
                .WithMany(a => a.Contacts)
                .HasForeignKey(l => l.ArtistID)
                .OnDelete(DeleteBehavior.Restrict) // Don't cascade delete for primary entities
                .IsRequired(false);

            // User relationship
            builder.HasOne(l => l.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(l => l.UserID)
                .OnDelete(DeleteBehavior.Restrict) // Don't cascade delete for primary entities
                .IsRequired(false);

            // Address relationship
            builder.HasOne(l => l.Address)
                .WithMany(a => a.Contacts)
                .HasForeignKey(l => l.AddressID)
                .OnDelete(DeleteBehavior.Cascade) // Contact info can be deleted if we delete the reference
                .IsRequired(false);

            // Phone Contact relationship
            builder.HasOne(l => l.PhoneContact)
                .WithMany(p => p.Contacts)
                .HasForeignKey(l => l.PhoneContactID)
                .OnDelete(DeleteBehavior.Cascade) // Contact info can be deleted if we delete the reference
                .IsRequired(false);

            // External Link relationship
            builder.HasOne(l => l.ExternalLink)
                .WithMany(e => e.Contacts)
                .HasForeignKey(l => l.ExternalLinkID)
                .OnDelete(DeleteBehavior.Cascade) // Contact info can be deleted if we delete the reference
                .IsRequired(false);
                
            // Seed the relationships
            SeedData(builder);
        }
        
        /// <summary>
        /// Seeds the initial relationships between artists/users and their contact information.
        /// </summary>
        /// <param name="builder">The entity type builder for Linker_UserAndArtistToContact.</param>
        private static void SeedData(EntityTypeBuilder<Linker_UserAndArtistToContact> builder)
        {
            // Link Studio Satarah (ArtistID=4) to their website (ExternalLinkID=5)
            builder.HasData(
                new Linker_UserAndArtistToContact
                {
                    Linker_UserAndArtistToContactID = 1,
                    ArtistID = 4, // Studio Satarah
                    ExternalLinkID = 5, // Their official website
                    Label = "Official Website",
                    MakePublic = true // Make this visible to the public
                }
            );

            // Link Studio Satarah (ArtistID=4) to their address (AddressID=4)
            builder.HasData(
                new Linker_UserAndArtistToContact
                {
                    Linker_UserAndArtistToContactID = 2,
                    ArtistID = 4, // Studio Satarah
                    AddressID = 4, // Their physical location
                    Label = "Studio Location",
                    MakePublic = true // Make this visible to the public
                }
            );

            // Link Studio Satarah (ArtistID=4) to their phone number (PhoneContactID=4)
            builder.HasData(
                new Linker_UserAndArtistToContact
                {
                    Linker_UserAndArtistToContactID = 3,
                    ArtistID = 4, // Studio Satarah
                    PhoneContactID = 4, // Charlotte Office phone number
                    Label = "Studio Phone",
                    MakePublic = true // Make this visible to the public
                }
            );
        }
    }
}
