using System.Diagnostics.CodeAnalysis;
using Disqord;

namespace Data.Entities.Tags;

public record TagOverview
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? ReferencedTagName { get; set; }
    public required Snowflake OwnerId { get; set; }
    public required Snowflake? GuildId { get; set; }

    public static TagOverview Create(Tag tag) => new(tag);

    public TagOverview() { }

    [SetsRequiredMembers]
    public TagOverview(Tag tag)
    {
        Name = tag.Name;
        Type = tag.GetType().Name;
        GuildId = tag.GuildId;
        OwnerId = tag.OwnerId;
        ReferencedTagName = (tag as AliasTag)?.ReferencedTag.Name;
    }
}