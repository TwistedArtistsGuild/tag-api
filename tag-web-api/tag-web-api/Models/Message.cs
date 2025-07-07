// <copyright file="Message.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;
public class Message
{
    [Key]
    public int MessageID { get; set; }

    public string DirMsg { get; set; }

    public bool Edited { get; set; }

    public int FromUserID { get; set; }

    public int? PictureID { get; set; }

    public DateTime Sent { get; set; }

    public int ToUserID { get; set; }

    // Add navigation properties
    public User FromUser { get; set; }

    public Picture Picture { get; set; }

    public User ToUser { get; set; }

    // Add other properties as needed
}
