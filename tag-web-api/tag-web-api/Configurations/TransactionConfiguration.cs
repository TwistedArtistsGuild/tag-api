// <copyright file="TransactionConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Models.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.TransactionID);
        
        // Use a simple configuration that should work regardless of the model's exact properties
        // Relationships that should be safe to assume
        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserID)
            .OnDelete(DeleteBehavior.SetNull);
            
        // Add timestamp columns with UTC awareness if they exist in your model
        if (builder.Metadata.FindProperty("Timestamp") != null) 
        {
            builder.Property("Timestamp")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}