using BotServices.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations;

public class TagAliasEntityConfiguration : IEntityTypeConfiguration<TagAlias>
{
    public void Configure(EntityTypeBuilder<TagAlias> builder)
    {
        builder.HasOne(x => x.ReferencedTag)
            .WithMany(x => x.Aliases)
            .HasForeignKey(x => x.ReferencedTagId);
    }
}