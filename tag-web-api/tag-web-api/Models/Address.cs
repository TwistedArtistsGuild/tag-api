// <copyright file="Address.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class Address
{
    [Key]
    public int AddressID { get; set; }

    [StringLength(255)]
    public string? AddressLine1 { get; set; }

    [StringLength(255)]
    public string? AddressLine2 { get; set; }
    
    public string? AddressLine3 { get; set; }

    public string? AddressLine4 { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? State { get; set; }

    [StringLength(20)]
    public string? ZipCode { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(100)]
    public string Region { get; set; }

    [StringLength(1000)]
    public string? OperationHours { get; set; }

    // Navigation property - contacts using this address
    public virtual ICollection<Linker_UserAndArtistToContact> Contacts { get; set; } = new List<Linker_UserAndArtistToContact>();
}
