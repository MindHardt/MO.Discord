using Data.Abstractions;
using Data.Entities;
using Disqord;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.Repositories;

public class EfCoreGuildRepository :
    EfCoreRepositoryBase<GuildData>,
    IGuildRepository
{
    public EfCoreGuildRepository(DbContext ctx) : base(ctx)
    {
    }

    public async ValueTask<GuildData?> FindGuild(Snowflake guildId)
    {
        return await Set.FirstOrDefaultAsync(u => u.GuildId == guildId);
    }

    public async ValueTask<GuildData> UpdateGuild(GuildData guildData)
    {
        var entry = await FindGuild(guildData.GuildId) is null ? 
            Set.Add(guildData) : 
            Set.Update(guildData);

        await CommitAsync();
        return entry.Entity;
    }
}