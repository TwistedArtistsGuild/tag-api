// <copyright file="Log.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;
public class Log
{
    [Key]
    public int LogID { get; set; }

    public int? ArtistID { get; set; }

    public bool Critical { get; set; }

    public int? ListingID { get; set; }

    public string? LoggedData { get; set; }

    public DateTime LogTimestamp { get; set; }

    public string? LongText { get; set; }

    [Required]
    public string ShortText { get; set; }

    public int? StaffID { get; set; }

    public string? Tags { get; set; }

    public int? UserID { get; set; }

    // Add navigation properties
    public Artist Artist { get; set; }
    public Listing Listing { get; set; }
    public Staff Staff { get; set; }
    public User User { get; set; }
}
