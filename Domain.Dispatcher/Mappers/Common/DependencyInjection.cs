using Domain.Dispatcher.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Dispatcher.Mappers.Common;

public static class DependencyInjection
{
    private static readonly Type[] MapperTypes =
    {
        typeof(ITypeMapper<,>), typeof(IMessageMapper<>)
    };
    
    /// <summary>
    /// Adds all implementations of <see cref="ITypeMapper{TSource,TResult}"/> to <paramref name="services"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>Reference to the same instance.</returns>
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IMappingProvider, MappingProvider>();
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(c => c.AssignableToAny(MapperTypes))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        
        return services;
    }
}