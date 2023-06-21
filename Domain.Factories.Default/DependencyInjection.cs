using Microsoft.Extensions.DependencyInjection;

namespace Domain.Factories.Default;

public static class DependencyInjection
{
    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        return services;
    }
}