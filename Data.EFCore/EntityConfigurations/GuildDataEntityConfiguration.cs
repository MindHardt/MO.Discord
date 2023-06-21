using Data.EFCore.Converters;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFCore.EntityConfigurations;

public class GuildDataEntityConfiguration : IEntityTypeConfiguration<GuildData>
{
    public void Configure(EntityTypeBuilder<GuildData> builder)
    {
        builder.HasKey(x => x.GuildId);
        builder.Property(x => x.GuildId)
            .ValueGeneratedNever()
            .HasConversion<SnowflakeConverter>();
            

        builder.Property(x => x.InlineTagsEnabled)
            .HasDefaultValue(false);
        
        builder.Property(x => x.InlineTagsPrefix)
            .HasDefaultValue("$");
    }
}