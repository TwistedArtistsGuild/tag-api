// <copyright file="BannedList.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models;
using System.ComponentModel.DataAnnotations;
public class BannedList
{
    [Key]
    public int BannedListID { get; set; }

    public int BannedReasonID { get; set; }
    public BannedReason BannedReason { get; set; } // Navigation property

    public string? Email { get; set; }

    public string? IPAddress { get; set; }

    public int? UserID { get; set; }
    public User User { get; set; } // Navigation property

    public string? Username { get; set; }
}
