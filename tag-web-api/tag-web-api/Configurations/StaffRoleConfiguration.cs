// <copyright file="StaffRoleConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class StaffRoleConfiguration : IEntityTypeConfiguration<StaffRole>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<StaffRole> builder)
        {
            builder.HasKey(sr => sr.StaffRoleID);

            builder.Property(sr => sr.Context)
                .HasColumnType("text");

            builder.Property(sr => sr.RoleName)
                .HasColumnType("text");

            // Seed data for roles
            builder.HasData(
                new StaffRole { StaffRoleID = 1, Context = "tag", RoleName = "Executive Director" },
                new StaffRole { StaffRoleID = 2, Context = "tag", RoleName = "Director" },
                new StaffRole { StaffRoleID = 3, Context = "tag", RoleName = "Manager" },
                new StaffRole { StaffRoleID = 4, Context = "tag", RoleName = "Moderator" },
                new StaffRole { StaffRoleID = 5, Context = "tag", RoleName = "FTE" },
                new StaffRole { StaffRoleID = 6, Context = "tag", RoleName = "PTE" }
            );
        }
    }
}
