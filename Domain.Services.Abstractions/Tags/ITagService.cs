using Data.Entities.Tags;
using Disqord;

namespace Domain.Services.Abstractions.Tags;

public interface ITagService
{
    /// <summary>
    /// Gets tag with name equal to <paramref name="tagName"/>
    /// which is visible in guild with id equal to <paramref name="guildId"/>
    /// or <see langword="null"/> if it is not found.
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="guildId"></param>
    /// <returns></returns>
    public Task<Tag?> GetTagAsync(string tagName, Snowflake guildId);
    
    /// <summary>
    /// Attempts to save <paramref name="tag"/> to the storage. This method can throw
    /// if <paramref name="userId"/> violates rights.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="userId"></param>
    /// <returns>Reference to the same instance once the operation is completed.</returns>
    public Task<Tag> SaveTagAsync(Tag tag, Snowflake userId);
}