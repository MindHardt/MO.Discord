using Domain.Dispatcher.Core;
using Domain.Dispatcher.Handlers.Tags;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Dispatcher.Default;

public static class DependencyInjection
{
    public static IServiceCollection AddCommandDispatcher(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<GetTagRequestHandler>();
        });
        services.AddFormatters();

        return services;
    }
    
    private static IServiceCollection AddFormatters(this IServiceCollection services)
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