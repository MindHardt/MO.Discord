using BotServices.Entities.Tags;
using Disqord;

namespace BotServices.Factories.Core;

public interface ITagFactory
{
    /// <summary>
    /// Creates a <see cref="TagMessage"/> with specified parameters. 
    /// </summary>
    /// <param name="name">The name of a new <see cref="Tag"/>.</param>
    /// <param name="text">The content of a new <see cref="Tag"/>.</param>
    /// <param name="ownerId">The owner of a new <see cref="Tag"/>.</param>
    /// <param name="guildId">The guild id of a new <see cref="Tag"/>.</param>
    /// <returns></returns>
    public TagMessage CreateTagMessage(string name, string text, Snowflake ownerId, Snowflake? guildId);

    /// <summary>
    /// Creates a <see cref="TagMessage"/> with specified parameters. 
    /// </summary>
    /// <param name="name">The name of a new <see cref="Tag"/>.</param>
    /// <param name="message">The message that will be a new <see cref="Tag"/>s content.</param>
    /// <param name="ownerId">The owner of a new <see cref="Tag"/>.</param>
    /// <param name="guildId">The guild id of a new <see cref="Tag"/>.</param>
    /// <returns></returns>
    public TagMessage CreateTagMessage(string name, IMessage message, Snowflake ownerId, Snowflake? guildId);

    /// <summary>
    /// Creates a new tag alias that refers to <paramref name="original"/>.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="newName"></param>
    /// <param name="ownerId"></param>
    /// <param name="guildId"></param>
    /// <returns></returns>
    public TagAlias CreateTagAlias(TagMessage original, string newName, Snowflake ownerId, Snowflake? guildId);
}