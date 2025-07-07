// <copyright file="CompetitionWinnerList.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class CompetitionWinnerList
{
    [Key]
    public int CompetitionWinnerListID { get; set; }

    public int Place { get; set; }

    public int TopTenPercentListingID { get; set; }

    public int VotesForListing { get; set; }

    public CompetitionListing CompetitionListing { get; set; }
}
