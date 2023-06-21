using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services.Default;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
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