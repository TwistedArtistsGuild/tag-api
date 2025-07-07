// <copyright file="CompetitionListing.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class CompetitionListing
{
    [Key]
    public int CompetitionListingID { get; set; }

    public int CompetitionID { get; set; }

    public int ListingID { get; set; }

    public Competition Competition { get; set; }
    public Listing Listing { get; set; }
}
