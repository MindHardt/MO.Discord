using BotServices.CQRS.Dispatcher.Core;
using BotServices.CQRS.Handlers.Tags;
using BotServices.CQRS.ResponseFormatters.Core;
using BotServices.CQRS.ResponseFormatters.Default;
using Microsoft.Extensions.DependencyInjection;

namespace BotServices.CQRS.Dispatcher.Default;

public static class DependencyInjection
{
    /// <summary>
    /// Adds default implementation of <see cref="IDiscordCommandDispatcher"/>
    /// and all the required services
    /// to the <paramref name="services"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDefaultCommandDispatcher(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<GetTagHandler>();
        });

        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(IDiscordResponseFormatter<>))
                .AddClasses(c => c.AssignableTo(typeof(IDiscordResponseFormatter<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        services.AddScoped<IDiscordExceptionFormatter, DefaultDiscordExceptionFormatter>();
        services.AddScoped<IDiscordCommandDispatcher, DefaultDiscordCommandDispatcher>();
        
        return services;
    }
}