// <copyright file="Vote.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        [Key]
        public int VoteID { get; set; }

        public string? Comment { get; set; }

        public int ResolutionID { get; set; }

        public DateTime Timestamp { get; set; }

        public string? VoteChoiceMultiple { get; set; }

        public bool? VoteChoiceTF { get; set; }

        public int VoterID { get; set; } 

        public Resolution Resolution { get; set; }
        public Artist Voter { get; set; } // Add this navigation property
    }
}
