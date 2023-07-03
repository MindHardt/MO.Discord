using Data.Entities.Tags;
using Disqord;

namespace Data.Entities;

public record GuildData
{
    public required Snowflake GuildId { get; set; }
    public bool InlineTagsEnabled { get; set; } = false;
    public string InlineTagsPrefix { get; set; } = "$";
    
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}