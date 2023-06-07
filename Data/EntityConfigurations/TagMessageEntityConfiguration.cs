using BotServices.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations;

public class TagMessageEntityConfiguration : IEntityTypeConfiguration<TagMessage>
{
    public void Configure(EntityTypeBuilder<TagMessage> builder)
    {
        builder.Property(x => x.Text)
            .HasMaxLength(Constants.MaxContentLength);

        builder.HasMany(x => x.Aliases)
            .WithOne(x => x.ReferencedTag)
            .HasForeignKey(x => x.ReferencedTagId);
    }
}