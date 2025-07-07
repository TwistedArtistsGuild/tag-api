// <copyright file="Resolution.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;
public class Resolution
{
    [Key]
    public int ResolutionID { get; set; }

    public string Body { get; set; }

    public DateTime? CanceledDate { get; set; }

    public string CanceledReason { get; set; }

    public DateTime DueDate { get; set; }

    public bool Executed { get; set; }

    public DateTime? ExecutedDate { get; set; }

    public bool MultipleChoice { get; set; }

    public string Subject { get; set; }

    public DateTime Timestamp { get; set; }

    public string Title { get; set; }
}