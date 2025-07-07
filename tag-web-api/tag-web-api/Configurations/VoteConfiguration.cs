// filepath: c:\src\azure_projects\tag-web-api-dotnet\tag-web-api\tag-web-api\Configurations\VoteConfiguration.cs
// <copyright file="VoteConfiguration.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;

    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(v => v.VoteID);

            builder.Property(v => v.Comment)
                .HasColumnType("text");

            builder.Property(v => v.Timestamp)
                .IsRequired();

            builder.HasOne<Artist>()
                .WithMany()
                .HasForeignKey(v => v.VoterID);

            builder.HasOne<Resolution>()
                .WithMany()
                .HasForeignKey(v => v.ResolutionID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
