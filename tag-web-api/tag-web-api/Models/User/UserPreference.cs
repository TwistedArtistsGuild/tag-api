// <copyright file="UserPreference.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models;
public class UserPreference
{
    [Key]
    public int UserPreferenceID { get; set; }

    public string MetricOrImperial { get; set; }

    public string ThemePreference { get; set; }

    public int UserID { get; set; }
    public User User { get; set; }
}
