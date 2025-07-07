

namespace TAGWEBAPI.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TAGWEBAPI.Models;
    
    public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
    {
        public void Configure(EntityTypeBuilder<TicketType> builder)
        {
            builder.HasKey(tt => tt.TicketTypeID);

            builder.Property(tt => tt.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(tt => tt.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new TicketType { TicketTypeID = 1, Description = "Gallery Showing", Type = "gallery showing" },
                new TicketType { TicketTypeID = 2, Description = "Live Band", Type = "live band" },
                new TicketType { TicketTypeID = 3, Description = "DJ Event", Type = "DJ event" },
                new TicketType { TicketTypeID = 4, Description = "Circus Arts", Type = "circus arts" },
                new TicketType { TicketTypeID = 5, Description = "Aerial Arts", Type = "aerial arts" },
                new TicketType { TicketTypeID = 6, Description = "Festival", Type = "a festival" },
                new TicketType { TicketTypeID = 7, Description = "Class", Type = "a class" },
                new TicketType { TicketTypeID = 8, Description = "Open Studio Period", Type = "an open studio period" }
            );
        }
    }
}
