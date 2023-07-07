using Data.Entities.Tags;
using Disqord;

namespace Data.Entities;

public record GuildData
{
    public required Snowflake GuildId { get; set; }
    public string GuildName { get; set; } = string.Empty;
    public bool InlineTagsEnabled { get; set; } = false;
    public string InlineTagsPrefix { get; set; } = "$";
    public bool AdultAllowed { get; set; } = false;
    
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}