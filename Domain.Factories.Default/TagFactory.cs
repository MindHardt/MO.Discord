using Data.Entities.Tags;
using Data.Entities.Users;
using Disqord;
using Domain.Factories.Abstractions;

namespace Domain.Factories.Default;

public class TagFactory : ITagFactory
{
    public MessageTag CreateMessageTag(string text, string name, UserData owner, Snowflake? guildId) => new()
    {
        Text = text,
        Name = name,
        Owner = owner,
        OwnerId = owner.UserId,
        GuildId = guildId
    };

    public AliasTag CreateAliasTag(Tag original, string name, UserData owner, Snowflake? guildId) => new()
    {
        ReferencedTag = original,
        Name = name,
        Owner = owner,
        OwnerId = owner.UserId,
        GuildId = guildId
    };
}