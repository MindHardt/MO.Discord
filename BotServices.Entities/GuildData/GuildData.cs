using Disqord;

namespace BotServices.Entities.GuildData;

public record GuildData
{
    public Snowflake GuildId { get; set; }
    public bool InlineTagsEnabled { get; set; } = false;
}