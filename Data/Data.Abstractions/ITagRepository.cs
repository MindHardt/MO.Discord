using Data.Entities.Tags;
using Disqord;

namespace Data.Abstractions;

public interface ITagRepository
{
    /// <summary>
    /// Finds tag by its <see cref="Tag.Name"/>. 
    /// </summary>
    /// <param name="name">The case-insensitive tag name.</param>
    /// <param name="guildId">The id of guild in which this tag must be visible.</param>
    /// <returns>The found <see cref="Tag"/> of <see langword="null"/> if none is found.</returns>
    public ValueTask<Tag?> FindTag(string name, Snowflake? guildId);

    /// <summary>
    /// Gets at most <paramref name="limit"/> overviews of <see cref="Tag"/>s
    /// that are visible in <paramref name="guildId"/> and whose names are similar to
    /// <paramref name="prompt"/>.
    /// </summary>
    /// <param name="prompt">The part of the <see cref="Tag"/>s name.</param>
    /// <param name="guildId">The id of the discord guild in which this tag must be visible.</param>
    /// <param name="limit">The maximum amount of <see cref="TagOverview"/>s fetched by this operation.</param>
    /// <returns></returns>
    public ValueTask<IReadOnlyCollection<TagOverview>> GetOverviews(string? prompt, Snowflake? guildId, int limit);

    /// <summary>
    /// Saves <paramref name="tag"/> to the storage.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public ValueTask<Tag> SaveTag(Tag tag);

    /// <summary>
    /// Gets the total amount of <see cref="Tag"/>s that user owns.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public ValueTask<int> CountTagsOf(Snowflake userId);
}