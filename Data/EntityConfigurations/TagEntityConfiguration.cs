using BotServices.Entities.Tags;
using Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations;

public class TagEntityConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.GuildId, x.Name })
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(Constants.MaxNameLength);

        builder.Property(x => x.GuildId)
            .HasConversion<SnowflakeConverter>();
        builder.Property(x => x.OwnerId)
            .HasConversion<SnowflakeConverter>();

        builder.HasDiscriminator(x => x.Discriminator)
            .HasValue<TagMessage>(nameof(TagMessage))
            .HasValue<TagAlias>(nameof(TagAlias));
    }
}