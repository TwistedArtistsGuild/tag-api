// <copyright file="PhoneContact.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class PhoneContact
{
    [Key]
    public int PhoneContactID { get; set; }

    [StringLength(50)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    // Navigation property - contacts using this phone
    public virtual ICollection<Linker_UserAndArtistToContact> Contacts { get; set; } = new List<Linker_UserAndArtistToContact>();
}
