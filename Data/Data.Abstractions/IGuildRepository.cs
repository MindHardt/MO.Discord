using Data.Entities;
using Disqord;

namespace Data.Abstractions;

public interface IGuildRepository
{
    /// <summary>
    /// Attempts to find <see cref="GuildData"/> that correlates with <paramref name="guildId"/>.
    /// </summary>
    /// <param name="guildId"></param>
    /// <returns>The found <see cref="GuildData"/> or <see langword="null"/> if none is found.</returns>
    public ValueTask<GuildData?> FindGuild(Snowflake guildId);

    /// <summary>
    /// Saves <paramref name="guildData"/> to the storage.
    /// </summary>
    /// <param name="guildData"></param>
    /// <returns></returns>
    public ValueTask<GuildData> UpdateGuild(GuildData guildData);
}