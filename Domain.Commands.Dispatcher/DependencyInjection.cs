using Domain.Commands.Formatters;
using Domain.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Commands.Dispatcher;

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
}