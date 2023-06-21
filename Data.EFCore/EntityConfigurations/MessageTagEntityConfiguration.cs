using Data.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFCore.EntityConfigurations;

public class MessageTagEntityConfiguration : IEntityTypeConfiguration<MessageTag>
{
    public void Configure(EntityTypeBuilder<MessageTag> builder)
    {
        builder.Property(x => x.Text)
            .HasMaxLength(MessageTag.MaxTextLength);
    }
}