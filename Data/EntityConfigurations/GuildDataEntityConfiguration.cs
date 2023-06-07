using BotServices.Entities.GuildData;
using Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations;

public class GuildDataEntityConfiguration : IEntityTypeConfiguration<GuildData>
{
    public void Configure(EntityTypeBuilder<GuildData> builder)
    {
        builder.HasKey(x => x.GuildId);
        builder.Property(x => x.GuildId)
            .ValueGeneratedNever()
            .HasConversion<SnowflakeConverter>();
    }
}