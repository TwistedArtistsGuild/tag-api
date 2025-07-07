// <copyright file="Blog.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAGWEBAPI.Models;

public class Blog
{
    [Key]
    public int BlogID { get; set; }

    [Required]
    public string Body { get; set; }

    public string? Body_Plaintext { get; set; }

    [Required]
    public string Byline { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    [Required]
    public string Path { get; set; }

    [Required]
    public string Title { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }

    public virtual User User { get; set; }
}
