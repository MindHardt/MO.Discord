using Disqord;

namespace Data.Entities;

public record GuildData
{
    public required Snowflake GuildId { get; set; }
    public required bool InlineTagsEnabled { get; set; }
    public required string InlineTagsPrefix { get; set; }
}