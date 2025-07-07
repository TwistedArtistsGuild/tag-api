// <copyright file="StaffRole.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models
{
    public class StaffRole
    {
        [Key]
        public int StaffRoleID { get; set; }

        public string Context { get; set; }

        public string RoleName { get; set; }

        // Navigation property
        public ICollection<Staff> Staffs { get; set; }
    }
}
