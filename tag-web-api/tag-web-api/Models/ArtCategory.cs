// <copyright file="ArtCategory.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAGWEBAPI.Models;

public class ArtCategory
{
    [Key]
    public int ArtCategoryID { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string CategoryKey { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Tags { get; set; }

    [ForeignKey("ParentCategory")]
    public int? ParentArtCategoryID { get; set; }

    public virtual ArtCategory? ParentCategory { get; set; }

    public virtual ICollection<ArtCategory>? SubCategories { get; set; }
}
