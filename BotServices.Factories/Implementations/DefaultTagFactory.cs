using BotServices.Entities.Tags;
using BotServices.Factories.Core;
using Disqord;

namespace BotServices.Factories.Implementations;

[Factory]
public class DefaultTagFactory : ITagFactory
{
    public TagMessage CreateTagMessage(string name, string text, Snowflake ownerId, Snowflake? guildId) => new()
    {
        Name = name,
        OwnerId = ownerId,
        GuildId = guildId,
        Text = text,
    };
    
    public TagMessage CreateTagMessage(string name, IMessage message, Snowflake ownerId, Snowflake? guildId) => new()
    {
        Name = name,
        OwnerId = ownerId,
        GuildId = guildId,
        Text = GetTagContentFromMessage(message)
    };

    public TagAlias CreateTagAlias(TagMessage original, string newName, Snowflake ownerId, Snowflake? guildId) => new()
    {
        ReferencedTag = original,
        ReferencedTagId = original.Id,
        Name = newName,
        OwnerId = ownerId,
        GuildId = guildId
    };

    private string GetTagContentFromMessage(IMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Content) is false) 
            return message.Content;

        if (message is not IUserMessage userMessage || userMessage.Attachments.Any() is false)
            throw new InvalidOperationException("Из этого сообщения нельзя сделать тег!");

        return userMessage.Attachments[0].Url;
    }
}