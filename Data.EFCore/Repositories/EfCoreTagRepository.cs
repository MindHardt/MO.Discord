using System.Text.RegularExpressions;
using Data.Abstractions;
using Data.Entities.Tags;
using Disqord;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.Repositories;

public class EfCoreTagRepository :
    EfCoreRepositoryBase<Tag>,
    ITagRepository
{
    public EfCoreTagRepository(DbContext ctx) : base(ctx)
    { }


    public async ValueTask<Tag?> FindTag(string name, Snowflake? guildId)
    {
        return await Set
            .Include(t => ((AliasTag)t).ReferencedTag)
            .Where(t => t.GuildId == null || t.GuildId == guildId)
            .FirstOrDefaultAsync(t => EF.Functions.ILike(t.Name, name));
    }

    public async ValueTask<IReadOnlyCollection<TagOverview>> GetOverviews(string? prompt, Snowflake? guildId, int limit)
    {
        return await Set
            .Where(t => t.GuildId == null || t.GuildId == guildId)
            .Where(t => Regex.IsMatch(t.Name, prompt ?? string.Empty))
            .OrderBy(t => EF.Functions.Random())
            .Take(limit)
            .Select(t => TagOverview.Create(t))
            .ToArrayAsync();
    }

    public async ValueTask<Tag> SaveTag(Tag tag)
    {
        var entry = Set.Add(tag);
        await CommitAsync();
        return entry.Entity;
    }

    public async ValueTask<int> CountTagsOf(Snowflake userId)
    {
        return await Set
            .Where(t => t.OwnerId == userId)
            .CountAsync();
    }
}