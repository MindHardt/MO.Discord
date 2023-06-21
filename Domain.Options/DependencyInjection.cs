using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddTypedOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DiscordOptions>(config.GetSection("Discord").Bind);
        services.Configure<CacheOptions>(config.GetSection("Cache").Bind);
        
        return services;
    }
}