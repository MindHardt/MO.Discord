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
        AddFormatters(services);

        return services;
    }
    
    private static void AddFormatters(IServiceCollection services)
    {
        services.AddScoped<IAggregateFormatter, AggregateFormatter>();
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(c => c.AssignableToAny(typeof(IFormatter<,>), typeof(IExceptionFormatter<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
    }
}