using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TAGWEBAPI.Models.Configurations;

public class MasterImpressionConfiguration : IEntityTypeConfiguration<MasterImpression>
{
    public void Configure(EntityTypeBuilder<MasterImpression> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasData(
            new MasterImpression { Id = 1, Emoji = "❤️", Name = "love", Label = "Love", DisplayOrder = 1 },
            new MasterImpression { Id = 2, Emoji = "👏", Name = "applause", Label = "Applause", DisplayOrder = 2 },
            new MasterImpression { Id = 3, Emoji = "🔥", Name = "fire", Label = "Fire", DisplayOrder = 3 },
            new MasterImpression { Id = 4, Emoji = "🙏", Name = "gratitude", Label = "Gratitude", DisplayOrder = 4 },
            new MasterImpression { Id = 5, Emoji = "🎨", Name = "art", Label = "Art", DisplayOrder = 5 },
            new MasterImpression { Id = 6, Emoji = "✨", Name = "sparkles", Label = "Sparkles", DisplayOrder = 6 },
            new MasterImpression { Id = 7, Emoji = "👀", Name = "eyes", Label = "Eyes", DisplayOrder = 7 },
            new MasterImpression { Id = 8, Emoji = "💯", Name = "hundred", Label = "Hundred", DisplayOrder = 8 },
            new MasterImpression { Id = 9, Emoji = "🤩", Name = "star_struck", Label = "Star Struck", DisplayOrder = 9 },
            new MasterImpression { Id = 10, Emoji = "🙌", Name = "praise", Label = "Praise", DisplayOrder = 10 }
            // Add the remaining 2 based on your requirements
        );
    }
}