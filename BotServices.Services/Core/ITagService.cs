using BotServices.Entities.Tags;
using Disqord;

namespace BotServices.Services.Core;

public interface ITagService : IBotService
{
    /// <summary>
    /// Saves <paramref name="tag"/> to the storage.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="authorId">Author id to check if they can edit the specified tag.</param>
    /// <param name="authorAdmin">Defines whether author is admin and can skip rights check.</param>
    /// <returns></returns>
    public Task SaveTagAsync(Tag tag, Snowflake authorId, bool authorAdmin);
    
    /// <summary>
    /// Gets tags that are visible in guild with <paramref name="guildId"/> and
    /// whose name is like <paramref name="prompt"/>. Their texts are excluded.
    /// </summary>
    /// <param name="guildId"></param>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<Tag>> GetTagsAsync(Snowflake? guildId, string? prompt);
    
    /// <summary>
    /// Gets tag with specified <paramref name="name"/>.
    /// If it is not found or is not visible in guild with <paramref name="guildId"/>
    /// then <see langword="null"/> is returned.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="guildId"></param>
    /// <returns></returns>
    public Task<Tag?> GetTagAsync(string name, Snowflake? guildId);

    /// <summary>
    /// Gets tag names that are visible in guild with <paramref name="guildId"/>
    /// and match the <paramref name="prompt"/>.
    /// </summary>
    /// <param name="guildId"></param>
    /// <param name="prompt"></param>
    /// <param name="editableBy">If specified, results will be filtered to tags editable by this user.</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<string>> GetTagNames(Snowflake? guildId, string prompt, Snowflake? editableBy = null);

    /// <summary>
    /// Deletes tag specified by <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="authorId"></param>
    /// <returns></returns>
    public Task<Tag> DeleteTagAsync(string name, Snowflake authorId);

    /// <summary>
    /// Converts <paramref name="tag"/>s content into <typeparamref name="TMessage"/>.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public TMessage CreateMessage<TMessage>(Tag tag)
        where TMessage : LocalMessageBase, new();
    
    /// <summary>
    /// Formats <paramref name="tag"/> in a readable form for display.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public string CreateOverview(Tag tag);
}