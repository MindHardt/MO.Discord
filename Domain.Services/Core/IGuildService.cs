using Data.Entities;
using Disqord;

namespace Domain.Services.Core;

public interface IGuildService
{
    /// <summary>
    /// Gets the saved <see cref="GuildData"/> with specified <paramref name="guildId"/>
    /// or creates a new one if none is found.
    /// </summary>
    /// <param name="guildId"></param>
    /// <returns></returns>
    public ValueTask<GuildData> GetOrCreateAsync(Snowflake guildId);

    /// <summary>
    /// Updates <paramref name="data"/> in the storage.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public ValueTask<GuildData> UpdateGuildAsync(GuildData data);
}