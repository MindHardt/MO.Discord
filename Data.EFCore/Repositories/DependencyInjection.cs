using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.EFCore.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddEntityFrameworkCoreRepositories<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped<DbContext, TContext>();
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(c => c.AssignableTo(typeof(EfCoreRepositoryBase<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        
        return services;
    }
}