using Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddEntityFrameworkCoreRepositories<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.Scan(scan =>
        {
            services.AddScoped<DbContext, TContext>();
            scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(EfCoreRepositoryBase<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        
        return services;
    }
}