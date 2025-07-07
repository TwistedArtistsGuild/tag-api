// <copyright file="Staff.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Models
{
    using System;
    using System.Collections.Generic;

    public class Staff
    {
        [Key]
        public int StaffID { get; set; }

        public bool Active { get; set; }

        public DateTime? LeaveDate { get; set; }

        public string Note { get; set; }

        public int StaffRoleID { get; set; }

        public DateTime StartDate { get; set; }

        public int UserID { get; set; }

        // Navigation properties
        public User User { get; set; }

        public StaffRole StaffRole { get; set; }
    }
}
