// <copyright file="DigitalDeliverySpecs.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class DigitalDeliverySpecs
{
    [Key]
    public int DigitalDeliverySpecsID { get; set; }

    public string? DeliveryURL { get; set; }

    public decimal? Duration { get; set; }

    [Required]
    public string LeadTime { get; set; }

    [Required]
    public string PromiseDescription { get; set; }
}
