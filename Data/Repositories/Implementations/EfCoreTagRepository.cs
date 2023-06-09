using System.Text.RegularExpressions;
using BotServices.Entities.Tags;
using Data.Repositories.Core;
using Disqord;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations;

public class EfCoreTagRepository :
    EfCoreRepositoryBase<Tag>,
    ITagRepository
{
    public EfCoreTagRepository(DbContext ctx) : base(ctx)
    {
    }

    public async Task<IReadOnlyCollection<Tag>> GetTagsAsync(int maxCount, Snowflake? guildId, string? prompt)
    {
        var query = Set
            .Include(t => ((TagAlias)t).ReferencedTag)
            .Where(t => t.GuildId == null || t.GuildId == guildId);

        if (prompt is not null)
            query = query.Where(t => Regex.IsMatch(t.Name, prompt));
        
        query = query
            .OrderBy(t => t.Name)
            .Take(maxCount);

        return await query.ToArrayAsync();
    }

    public async Task SaveTagAsync(Tag tag)
    {
        Set.Update(tag);
        await CommitAsync();
    }

    public async Task<Tag?> GetTagAsync(string name)
    {
        return await Set
            .Include(t => ((TagAlias)t).ReferencedTag)
            .FirstOrDefaultAsync(t => EF.Functions.ILike(t.Name, name));
    }

    public async Task<IReadOnlyCollection<string>> GetTagNamesAsync(int maxCount, Snowflake? guildId, string prompt, Snowflake? editableBy)
    {
        var query = Set
            .Where(t => t.GuildId == null || t.GuildId == guildId)
            .Where(t => Regex.IsMatch(t.Name, prompt));

        if (editableBy.HasValue)
        {
            query = query.Where(t => t.OwnerId == editableBy.Value);
        }

        return await query
            .Select(t => t.Name)
            .OrderBy(_ => EF.Functions.Random())
            .Take(maxCount)
            .ToArrayAsync();
    }

    public async Task DeleteTagAsync(Tag tag)
    {
        Set.Remove(tag);
        await CommitAsync();
    }
}