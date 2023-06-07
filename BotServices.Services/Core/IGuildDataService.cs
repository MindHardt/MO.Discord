using BotServices.Entities.GuildData;
using Disqord;

namespace BotServices.Services.Core;

public interface IGuildDataService : IBotService
{
    /// <summary>
    /// Gets <see cref="GuildData"/> for one guild.
    /// </summary>
    /// <param name="guildId"></param>
    /// <returns>A <see cref="GuildData"/> for specified guild of <see langword="null"/> if none is found.</returns>
    public Task<GuildData?> GetGuildDataAsync(Snowflake guildId);
    /// <summary>
    /// Gets <see cref="GuildData"/> for one guild.
    /// </summary>
    /// <param name="guild"></param>
    /// <returns>A <see cref="GuildData"/> for specified guild of <see langword="null"/> if none is found.</returns>
    public Task<GuildData?> GetGuildDataAsync(IGuild guild) 
        => GetGuildDataAsync(guild.Id);
    
    /// <summary>
    /// Gets <see cref="GuildData"/>s for all guilds specified by <paramref name="guildIds"/>.
    /// </summary>
    /// <param name="guildIds"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GuildData>> GetAllGuildsDataAsync(IEnumerable<Snowflake> guildIds);
    /// <summary>
    /// Gets <see cref="GuildData"/>s for all guilds in <paramref name="guilds"/>.
    /// </summary>
    /// <param name="guilds"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GuildData>> GetAllGuildsDataAsync(IEnumerable<IGuild> guilds)
        => GetAllGuildsDataAsync(guilds.Select(g => g.Id));

    /// <summary>
    /// Saves <paramref name="data"/> to the storage.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task SaveGuildDataAsync(GuildData data);
}