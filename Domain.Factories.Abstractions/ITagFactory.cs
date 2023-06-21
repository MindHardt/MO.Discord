using Data.Entities.Tags;
using Data.Entities.Users;
using Disqord;

namespace Domain.Factories.Abstractions;

public interface ITagFactory
{
    public MessageTag CreateMessageTag(string text, string name, UserData owner, Snowflake? guildId);
    public AliasTag CreateAliasTag(Tag original, string name, UserData owner, Snowflake? guildId);
}