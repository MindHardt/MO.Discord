using Microsoft.Extensions.DependencyInjection;

namespace BotServices.Services.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddBotServices(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(c => c.AssignableTo<IBotService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        return services;
    }
        
}