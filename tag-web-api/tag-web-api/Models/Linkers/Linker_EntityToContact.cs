// <copyright file="Linker_EntityToContact.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAGWEBAPI.Models;

public enum ContactScope
{
    Private = 0,
    Primary = 1,
    Secondary = 2
}

public class Linker_EntityToContact
{
    [Key]
    public int Linker_EntityToContactID { get; set; }

    public int ContactID { get; set; }

    public int? UserID { get; set; }

    public int? ArtistID { get; set; }

    public int? VenueID { get; set; }

    public int? VendorID { get; set; }

    public ContactScope Scope { get; set; } = ContactScope.Secondary;

    public int DisplayOrder { get; set; } = 0;

    [ForeignKey("ContactID")]
    public Contact Contact { get; set; } = null!;

    [ForeignKey("UserID")]
    public User? User { get; set; }

    [ForeignKey("ArtistID")]
    public Artist? Artist { get; set; }

    [ForeignKey("VenueID")]
    public Venue? Venue { get; set; }

    [ForeignKey("VendorID")]
    public Vendor? Vendor { get; set; }
}