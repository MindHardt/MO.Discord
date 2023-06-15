using BotServices.Autocompletes.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BotServices.Autocompletes.Default;

public static class DependencyInjection
{
    public static IServiceCollection AddAutocompletes(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(c => c.AssignableTo(typeof(IAutocompleteProvider<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(c => c.AssignableTo(typeof(IAutocomplete<,>)))
                .AsSelf()
                .WithScopedLifetime();
        });
        
        return services;
    }
}