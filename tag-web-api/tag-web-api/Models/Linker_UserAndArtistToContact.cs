// <copyright file="Linker_UserAndArtistToContact.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAGWEBAPI.Models;

public class Linker_UserAndArtistToContact
{
    [Key]
    public int Linker_UserAndArtistToContactID { get; set; }

    // Foreign keys with nullable properties - only one contact type must be set
    public int? AddressID { get; set; }
    
    public int? ArtistID { get; set; }
    
    public int? ExternalLinkID { get; set; }
    
    public int? PhoneContactID { get; set; }
    
    public int? UserID { get; set; }

    [StringLength(255)]
    public string? Label { get; set; }
    
    public bool MakePublic { get; set; } = false;

    // Navigation properties with correct nullable annotation
    [ForeignKey("AddressID")]
    public Address? Address { get; set; }

    [ForeignKey("ArtistID")]
    public Artist? Artist { get; set; }

    [ForeignKey("ExternalLinkID")]
    public ExternalLink? ExternalLink { get; set; }

    [ForeignKey("PhoneContactID")]
    public PhoneContact? PhoneContact { get; set; }

    [ForeignKey("UserID")]
    public User? User { get; set; }
}
