// <copyright file="CompetitionVoteList.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class CompetitionVoteList
{
    [Key]
    public int CompetitionVoteListID { get; set; }

    public string? Comments { get; set; }

    public int CompeitionListingID { get; set; }

    public DateTime Timestamp { get; set; }

    public int VoterID { get; set; }

    public CompetitionListing CompetitionListing { get; set; }
}
