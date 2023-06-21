using Disqord;

namespace Data.Entities.Tags;

public record TagOverview
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? ReferencedTagName { get; set; }
    public required Snowflake OwnerId { get; set; }
    public required Snowflake? GuildId { get; set; }
}