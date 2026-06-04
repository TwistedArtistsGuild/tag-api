// <copyright file="LinkerEntityToContactConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class LinkerEntityToContactConfiguration : IEntityTypeConfiguration<Linker_EntityToContact>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Linker_EntityToContact> builder)
    {
        builder.HasKey(l => l.Linker_EntityToContactID);

        builder.Property(l => l.Scope)
            .HasDefaultValue(ContactScope.Secondary);

        builder.Property(l => l.DisplayOrder)
            .HasDefaultValue(0);

        builder.HasOne(l => l.Contact)
            .WithMany(c => c.EntityLinks)
            .HasForeignKey(l => l.ContactID)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(l => l.Artist)
            .WithMany()
            .HasForeignKey(l => l.ArtistID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(l => l.Venue)
            .WithMany()
            .HasForeignKey(l => l.VenueID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(l => l.Vendor)
            .WithMany()
            .HasForeignKey(l => l.VendorID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_LinkerEntityToContact_SingleOwner",
                "(CASE WHEN \"UserID\" IS NOT NULL THEN 1 ELSE 0 END + " +
                "CASE WHEN \"ArtistID\" IS NOT NULL THEN 1 ELSE 0 END + " +
                "CASE WHEN \"VenueID\" IS NOT NULL THEN 1 ELSE 0 END + " +
                "CASE WHEN \"VendorID\" IS NOT NULL THEN 1 ELSE 0 END) = 1");
        });

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Linker_EntityToContact> builder)
    {
        builder.HasData(
            new Linker_EntityToContact
            {
                Linker_EntityToContactID = 1001,
                ArtistID = 4,
                ContactID = 1001,
                Scope = ContactScope.Primary,
                DisplayOrder = 1,
            },
            new Linker_EntityToContact
            {
                Linker_EntityToContactID = 1002,
                ArtistID = 4,
                ContactID = 1002,
                Scope = ContactScope.Secondary,
                DisplayOrder = 2,
            },
            new Linker_EntityToContact
            {
                Linker_EntityToContactID = 1003,
                ArtistID = 4,
                ContactID = 1003,
                Scope = ContactScope.Secondary,
                DisplayOrder = 3,
            },
            new Linker_EntityToContact
            {
                Linker_EntityToContactID = 1004,
                ArtistID = 4,
                ContactID = 1004,
                Scope = ContactScope.Secondary,
                DisplayOrder = 4,
            });
    }
}