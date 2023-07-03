using Data.EFCore.Converters;
using Data.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFCore.EntityConfigurations;

public class UserDataEntityConfiguration : IEntityTypeConfiguration<UserData>
{
    public void Configure(EntityTypeBuilder<UserData> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId)
            .ValueGeneratedNever()
            .HasConversion<SnowflakeConverter>();

        builder.Property(x => x.CustomTagLimit)
            .HasDefaultValue(null);

        builder.HasMany(x => x.Tags)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}