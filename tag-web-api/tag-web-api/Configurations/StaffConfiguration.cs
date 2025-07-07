// <copyright file="StaffConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(s => s.StaffID);

            builder.Property(s => s.Active)
                .IsRequired();

            builder.Property(s => s.LeaveDate)
                .HasColumnType("timestamptz");

            builder.Property(s => s.Note)
                .IsRequired(false)
                .HasColumnType("text");

            builder.Property(s => s.StaffRoleID)
                .IsRequired();

            builder.Property(s => s.StartDate)
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(s => s.UserID)
                .IsRequired();

            // Set delete behaviors to SetNull
            builder.HasOne(s => s.User)
                .WithMany(u => u.Staffs)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure foreign key relationship for StaffRoleID
            builder.HasOne(s => s.StaffRole)
                .WithMany(sr => sr.Staffs)
                .HasForeignKey(s => s.StaffRoleID)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed data for a single user assigned as an executive director
            builder.HasData(
                new Staff
                {
                    StaffID = 1,
                    UserID = 1,
                    StaffRoleID = 1,
                    Active = true,
                    StartDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc) // Ensure UTC timestamp
                }
            );
        }
    }
}
