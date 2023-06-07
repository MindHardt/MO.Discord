using BotServices.Entities.GuildData;
using Disqord;

namespace Data.Repositories.Core;

public interface IGuildDataRepository
{
    /// <summary>
    /// Gets <see cref="GuildData"/> for one guild specified by <paramref name="guildId"/>.
    /// </summary>
    /// <param name="guildId"></param>
    /// <returns>A <see cref="GuildData"/> for specified guild of <see langword="null"/> if none is found.</returns>
    public Task<GuildData?> GetGuildDataAsync(Snowflake guildId);
    
    /// <summary>
    /// Gets <see cref="GuildData"/>s for all guilds specified by <paramref name="guildIds"/>.
    /// </summary>
    /// <param name="guildIds"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GuildData>> GetAllGuildsDataAsync(IEnumerable<Snowflake> guildIds);

    /// <summary>
    /// Saves <paramref name="data"/> to the storage.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task SaveGuildDataAsync(GuildData data);
}