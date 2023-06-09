using Microsoft.Extensions.DependencyInjection;

namespace BotServices.Factories.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddBotFactories(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(c => c.WithAttribute<FactoryAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        return services;
    }
}