using Data.Abstractions;
using Data.Entities.Tags;
using Disqord;
using Domain.Exceptions;
using Domain.Options;
using Domain.Services.Core;
using Domain.Services.Core.Tags;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Domain.Services.Default.Tags;

public class TagService : ITagService
{
    private const int OverviewCount = Discord.Limits.ApplicationCommand.Option.MaxChoiceAmount;
    
    private readonly ITagRepository _tagRepository;
    private readonly IUserService _userService;
    private readonly IMemoryCache _cache;
    private readonly IOptions<CacheOptions> _cacheOptions;

    public TagService(
        ITagRepository tagRepository, 
        IUserService userService, 
        IMemoryCache cache, 
        IOptions<CacheOptions> cacheOptions)
    {
        _tagRepository = tagRepository;
        _userService = userService;
        _cache = cache;
        _cacheOptions = cacheOptions;
    }

    public async Task<Tag?> GetTagAsync(string tagName, Snowflake guildId)
    {
        string cacheName = GetCacheName(tagName);
        if (_cache.TryGetValue(cacheName, out Tag? tag))
        {
            return tag;
        }

        tag = await _tagRepository.FindTag(tagName, guildId);
        _cache.Set(cacheName, tag, _cacheOptions.Value.TagExpiration);
        return tag;
    }

    public async Task<Tag> SaveTagAsync(Tag tag, Snowflake userId)
    {
        AccessException.ThrowIf(await _userService.TagsLimitExceeded(userId));
        var existingTag = await _tagRepository.FindTag(tag.Name, tag.GuildId);

        if (existingTag is not null)
            throw new ArgumentException("Тег с таким названием уже существует!");

        return await _tagRepository.SaveTag(tag);
    }

    public async ValueTask<IReadOnlyCollection<TagOverview>> GetOverviews(string? prompt, Snowflake? guildId)
    {
        return await _tagRepository.GetOverviews(prompt, guildId, OverviewCount);
    }

    private static string GetCacheName(string tagName) => $"TAG_{tagName.ToUpper()}";
}