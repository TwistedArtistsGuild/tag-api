// <copyright file="ShippingSpecs.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;
public class ShippingSpecs
{
    [Key]
    public int ShippingSpecsID { get; set; }

    public string CarrierAccount { get; set; }

    public string DistanceUnit { get; set; }

    public bool HazardousShipping { get; set; }

    public decimal Height { get; set; }

    public string MassUnit { get; set; }

    public string PackageType { get; set; }

    public string ShippingNotes { get; set; }

    public decimal ShippingWeight { get; set; }

    public string ShippoObjectID { get; set; }

    public decimal ShipWeight { get; set; }

    public string Units { get; set; }

    public decimal Weight { get; set; }
}
