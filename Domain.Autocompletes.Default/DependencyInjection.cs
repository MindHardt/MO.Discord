using Domain.Autocompletes.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Autocompletes.Default;

public static class DependencyInjection
{
    public static IServiceCollection AddAutocompletes(this IServiceCollection services)
    {
        services.AddScoped<IAutocompleteProvider, AutocompleteProvider>();
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(c => c.AssignableTo(typeof(IAutocomplete<,>)))
                .AsSelf()
                .WithScopedLifetime();
        });
        
        return services;
    }
}