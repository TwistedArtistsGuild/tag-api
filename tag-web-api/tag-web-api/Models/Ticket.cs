// <copyright file="Ticket.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;
public class Ticket
{
    [Key]
    public int TicketID { get; set; }

    public string AttendeeName { get; set; }

    public DateTime SoldTimestamp { get; set; }

    public int TicketTypeID { get; set; }

    public string UniqueHash { get; set; }
}
