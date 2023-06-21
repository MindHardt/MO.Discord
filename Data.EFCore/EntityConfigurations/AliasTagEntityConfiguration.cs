using Data.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFCore.EntityConfigurations;

public class AliasTagEntityConfiguration : IEntityTypeConfiguration<AliasTag>
{
    public void Configure(EntityTypeBuilder<AliasTag> builder)
    {
        builder.HasOne(x => x.ReferencedTag)
            .WithMany(x => x.Aliases)
            .HasForeignKey(x => x.ReferencedTagId);
    }
}