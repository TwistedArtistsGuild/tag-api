// <copyright file="Artist.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAGWEBAPI.Models;

public enum BusinessEntityType
{
    Individual = 0,
    SoleProprietor = 1,
    Partnership = 2,
    LLC = 3,
    SCorp = 4,
    CCorp = 5,
    Other = 6,
}

public class Artist
{
    [Key]
    public int ArtistID { get; set; }

    [Required]
    public DateTime Applied { get; set; }

    public string? Biography { get; set; }

    [Required]
    public string Byline { get; set; }

    [Required]
    public string Path { get; set; }

    public string? SEOTags { get; set; }

    public DateTime? Since { get; set; }

    public string? Statement { get; set; }

    [Required]
    public string Title { get; set; }

    [ForeignKey("Picture")]
    public int? CoverPicID { get; set; }

    public virtual Picture? CoverPic { get; set; }

    [ForeignKey("ArtCategory")]
    public int? FocusCategoryID { get; set; }

    public virtual ArtCategory? FocusCategory { get; set; }

    [ForeignKey("Picture")]
    public int? ProfilePicID { get; set; }

    public virtual Picture? ProfilePic { get; set; }

    public virtual ArtistPermissions ArtistPermissions { get; set; }

    public virtual ICollection<Linker_UserAndArtistToContact>? Contacts { get; set; } = new List<Linker_UserAndArtistToContact>();

    // Business entity type for the artist (Individual, LLC, S-Corp, etc.)
    public BusinessEntityType BusinessEntity { get; set; } = BusinessEntityType.Individual;

    public void SetPath(string path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        Path = path.ToLowerInvariant();
    }
}
