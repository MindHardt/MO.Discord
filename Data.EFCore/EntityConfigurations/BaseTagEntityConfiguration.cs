using Data.EFCore.Converters;
using Data.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFCore.EntityConfigurations;

public class BaseTagEntityConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.GuildId, x.Name })
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(Tag.MaxNameLength);

        builder.Property(x => x.GuildId)
            .HasConversion<SnowflakeConverter>();
        builder.Property(x => x.OwnerId)
            .HasConversion<SnowflakeConverter>();

        builder.HasDiscriminator()
            .HasValue<MessageTag>(nameof(MessageTag))
            .HasValue<AliasTag>(nameof(AliasTag));

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}