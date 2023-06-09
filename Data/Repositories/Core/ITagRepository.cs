using BotServices.Entities.Tags;
using Disqord;

namespace Data.Repositories.Core;

public interface ITagRepository
{
    /// <summary>
    /// Fetches tags that are visible in guild whose id is <paramref name="guildId"/>.
    /// and name matches the <paramref name="prompt"/>.
    /// </summary>
    /// <param name="maxCount">The maximum amount of returned tags.</param>
    /// <param name="guildId">The id of the guild.</param>
    /// <param name="prompt">The part of tags name.</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<Tag>> GetTagsAsync(int maxCount, Snowflake? guildId, string? prompt);

    /// <summary>
    /// Saves <paramref name="tag"/> to the database.
    /// </summary>
    /// <param name="tag">The saved tag.</param>
    /// <returns></returns>
    public Task SaveTagAsync(Tag tag);

    /// <summary>
    /// Gets <see cref="Tag"/> with specified <paramref name="name"/>
    /// or <see langword="null"/> if none was found.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<Tag?> GetTagAsync(string name);

    /// <summary>
    /// Gets tag names that are visible in guild with <paramref name="guildId"/>
    /// and match the <paramref name="prompt"/>.
    /// </summary>
    /// <param name="maxCount"></param>
    /// <param name="guildId"></param>
    /// <param name="prompt"></param>
    /// <param name="editableBy"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<string>> GetTagNamesAsync(int maxCount, Snowflake? guildId, string prompt, Snowflake? editableBy);
    
    /// <summary>
    /// Deletes the tag with specified name.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public Task DeleteTagAsync(Tag tag);
}