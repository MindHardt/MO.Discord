using Microsoft.Extensions.DependencyInjection;

namespace Domain.Commands.Formatters;

public static class DependencyInjection
{
    public static IServiceCollection AddFormatters(this IServiceCollection services)
    {
        services.AddScoped<IAggregateFormatter, AggregateFormatter>();
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(c => c.AssignableTo(typeof(IFormatter<,>)))
                .AddClasses(c => c.AssignableTo(typeof(IExceptionFormatter<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        
        return services;
    }
}