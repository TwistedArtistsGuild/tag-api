// <copyright file="Competition.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;

public class Competition
{
    [Key]
    public int CompetitionID { get; set; }

    public DateTime DueDate { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Period { get; set; }

    [Required]
    public string Prompt { get; set; }

    public decimal? TotalVotePoints { get; set; }

    public ICollection<CompetitionListing> CompetitionListings { get; set; }
    public ICollection<CompetitionVoteList> CompetitionVoteLists { get; set; }
    public ICollection<CompetitionWinnerList> CompetitionWinnerLists { get; set; }
}
