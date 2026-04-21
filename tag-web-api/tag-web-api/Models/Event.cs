// <copyright file="Event.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAGWEBAPI.Models;
public class Event
{
    [Key]
    public int EventID { get; set; }

    public int ArtistID { get; set; }

    public decimal Cost { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime Doors { get; set; }

    public DateTime EndTime { get; set; }

    public int MaxOccupancy { get; set; }

    public int MinimumAge { get; set; }

    public string? Note { get; set; }

    [Required]
    public string PointOfContact { get; set; }

    public DateTime StartTime { get; set; }

    [Required]
    public string Title { get; set; }

    public int VenueID { get; set; }

    [Required]
    public string Path { get; set; }

    [ForeignKey("EventCategory")]
    public int? EventCategoryID { get; set; }
    
    // Navigation properties
    public Artist Artist { get; set; }

    public EventCategory? EventCategory { get; set; }
    
    public Venue Venue { get; set; }
}
