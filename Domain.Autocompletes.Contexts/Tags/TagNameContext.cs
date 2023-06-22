using Disqord;

namespace Domain.Autocompletes.Contexts.Tags;

public record TagNameContext
{
    public required Snowflake? GuildId { get; init; }
}