using BotServices.Entities.GuildData;
using Data.Repositories.Core;
using Disqord;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations;

public class EfCoreGuildDataRepository :
    EfCoreRepositoryBase<GuildData>,
    IGuildDataRepository
{
    public EfCoreGuildDataRepository(DbContext ctx) : base(ctx)
    {
    }

    public async Task<GuildData?> GetGuildDataAsync(Snowflake guildId)
    {
        return await Set
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.GuildId == guildId);
    }

    public async Task<IReadOnlyCollection<GuildData>> GetAllGuildsDataAsync(IEnumerable<Snowflake> guildIds)
    {
        return await Set
            .AsNoTracking()
            .Where(d => guildIds.Contains(d.GuildId))
            .ToArrayAsync();
    }

    public async Task SaveGuildDataAsync(GuildData data)
    {
        var existingData = await GetGuildDataAsync(data.GuildId);

        if (existingData is null)
        {
            Set.Add(data);
        }
        else
        {
            Set.Update(data);
        }

        await CommitAsync();
    }
}