using Data.Abstractions;
using Data.Entities;
using Disqord;
using Domain.Options;
using Domain.Services.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domain.Services.Default;

public class GuildService : IGuildService
{
    private readonly IGuildRepository _guildRepository;
    private readonly IMemoryCache _cache;
    private readonly IOptions<CacheOptions> _options;
    private readonly ILogger<GuildService> _logger;

    public GuildService(
        IGuildRepository guildRepository, 
        IMemoryCache cache, 
        IOptions<CacheOptions> options, 
        ILogger<GuildService> logger)
    {
        _guildRepository = guildRepository;
        _cache = cache;
        _options = options;
        _logger = logger;
    }

    public async ValueTask<GuildData> GetOrCreateAsync(Snowflake guildId)
    {
        string cacheName = GetCacheName(guildId);
        if (_cache.TryGetValue(cacheName, out GuildData? data) && data is not null)
        {
            _logger.LogInformation("Guild data {Id} retrieved from cache", guildId);
            return data;
        }

        data = await _guildRepository.FindGuild(guildId);
        
        if (data is null)
            _logger.LogInformation("Guild data {Id} not found in storage, creating...", guildId);
        else
            _logger.LogInformation("Guild data {Id} retrieved from storage", guildId);
        
        data ??= new GuildData { GuildId = guildId };
        _cache.Set(cacheName, data, _options.Value.GuildExpiration);

        return data;
    }

    public async ValueTask<GuildData> UpdateGuildAsync(GuildData data)
    {
        data = await _guildRepository.UpdateGuild(data);
        _cache.Set(GetCacheName(data.GuildId), data, _options.Value.GuildExpiration);
        
        _logger.LogInformation("Updated guild data {Id}", data.GuildId);

        return data;
    }

    private static string GetCacheName(Snowflake guildId) => $"GUILD_{guildId}";
}