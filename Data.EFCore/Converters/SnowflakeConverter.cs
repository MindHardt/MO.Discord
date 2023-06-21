using Disqord;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.EFCore.Converters;

public class SnowflakeConverter : ValueConverter<Snowflake, ulong>
{
    public SnowflakeConverter()
        : base(
            v => v,
            v => v)
    {
    }
}