using System.Text.RegularExpressions;
using BotServices.Entities.Tags;
using BotServices.Exceptions;
using BotServices.Services.Core;
using Data.Repositories.Core;
using Disqord;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BotServices.Services.Implementations;

public partial class TagService : ITagService
{
    private const int MaxTagCount = Discord.Limits.Message.MaxEmbedAmount;
    private const int MaxTagNamesCount = Discord.Limits.ApplicationCommand.Option.MaxChoiceAmount;
    private readonly ITagRepository _repo;
    private readonly IMemoryCache _cache;
    private readonly ILogger<TagService> _logger;
    private readonly ITagNameService _nameService;

    public TagService(
        ITagRepository repo,
        IMemoryCache cache, 
        ILogger<TagService> logger, ITagNameService nameService)
    {
        _repo = repo;
        _cache = cache;
        _logger = logger;
        _nameService = nameService;
    }

    public async Task SaveTagAsync(Tag tag, Snowflake authorId, bool authorAdmin)
    {
        if (_nameService.TagNameValid(tag.Name) is false)
            throw new InvalidOperationException("Это имя недопустимо для тега.");
        
        if (tag.Id != default && authorAdmin is false)
        {
            Tag? dbTag = await _repo.GetTagAsync(tag.Name);

            if (dbTag is not null && dbTag.OwnerId != authorId)
                throw new InvalidOperationException("You are not allowed to edit this tag.");
        }

        await _repo.SaveTagAsync(tag);
    }

    public Task<IReadOnlyCollection<Tag>> GetTagsAsync(Snowflake? guildId, string? prompt)
    {
        return _repo.GetTagsAsync(MaxTagCount, guildId, prompt);
    }

    public async Task<Tag?> GetTagAsync(string name, Snowflake? guildId)
    {
        var cacheName = GetCacheName(name);
        if (_cache.TryGetValue(cacheName, out Tag? tag))
        {
            _logger.LogInformation("Retrieved tag {Name} from cache", 
                name);
            return tag;
        }
        
        tag = await _repo.GetTagAsync(name);
        _cache.Set(cacheName, tag);
        
        _logger.LogInformation("Cached tag {TagName}", 
            name);

        if (tag?.IsPublic is false && tag.GuildId != guildId)
            return null;
            
        return tag;
    }

    public Task<IReadOnlyCollection<string>> GetTagNames(Snowflake? guildId, string prompt, Snowflake? editableBy = null)
    {
        return _repo.GetTagNamesAsync(MaxTagNamesCount, guildId, prompt, editableBy);
    }

    public async Task<Tag> DeleteTagAsync(string name, Snowflake authorId)
    {
        var dbTag = await _repo.GetTagAsync(name);
        
        NotFoundException.ThrowIfNull(dbTag);
        AccessException.ThrowIf(dbTag.OwnerId != authorId);

        await _repo.DeleteTagAsync(dbTag);
        _cache.Remove(GetCacheName(dbTag.Name));

        return dbTag;
    }

    public TMessage CreateMessage<TMessage>(Tag tag) where TMessage : LocalMessageBase, new()
    {
        return new TMessage().WithContent(tag.Content);
    }
    
    public string CreateOverview(Tag tag)
    {
        return $"{(tag.IsPublic ? "🔓" : "🔒")}" +
               $"{(tag is TagMessage ? "✉️" : "🔗")} | " +
               $"{tag.Name}";
    }

    private static string GetCacheName(string tagName) => $"TAG_{tagName.ToUpperInvariant()}";
}