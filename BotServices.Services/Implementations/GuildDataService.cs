using BotServices.Entities.GuildData;
using BotServices.Services.Core;
using Data.Repositories.Core;
using Disqord;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BotServices.Services.Implementations;

public class GuildDataService : IGuildDataService
{
    private readonly IGuildDataRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly ILogger<GuildDataService> _logger;

    public GuildDataService(
        IGuildDataRepository repository, 
        IMemoryCache cache, 
        ILogger<GuildDataService> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<GuildData?> GetGuildDataAsync(Snowflake guildId)
    {
        var cacheName = GetCacheName(guildId);
        if (_cache.TryGetValue(cacheName, out GuildData? data))
        {
            _logger.LogInformation("Retrieved guild data with id {Id} and value {Value} from cache", 
                guildId.RawValue, data);
            return data;
        }
        _logger.LogInformation("Guild data with id {Id} not found in cache", 
            guildId.RawValue);
        
        data = await _repository.GetGuildDataAsync(guildId);
        _cache.Set(cacheName, data);
        
        _logger.LogInformation("Cached guild data with id {Id} and value {Value}", 
            guildId.RawValue, data);
        
        return data;
    }

    public Task<IReadOnlyCollection<GuildData>> GetAllGuildsDataAsync(IEnumerable<Snowflake> guildIds)
    {
        return _repository.GetAllGuildsDataAsync(guildIds);
    }

    public Task SaveGuildDataAsync(GuildData data)
    {
        _cache.Set(GetCacheName(data.GuildId), data);
        return _repository.SaveGuildDataAsync(data);
    }

    private static string GetCacheName(Snowflake guildId) => $"GUILD_{guildId.RawValue}";
}