// <copyright file="BannedReason.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class BannedReason
{
    [Key]
    public int BannedReasonID { get; set; }

    public DateTime BannedOn { get; set; }

    [Required]
    public string PublicReasonForBan { get; set; }

    [Required]
    public string ReasonForBan { get; set; }

    public string? SupportingDocsURL { get; set; }
}
