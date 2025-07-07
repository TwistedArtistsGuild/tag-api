// <copyright file="TicketType.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    public class TicketType
    {
        [Key]
        public int TicketTypeID { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
    }
}
