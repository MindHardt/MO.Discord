using Data.EFCore;
using Data.EFCore.Repositories;
using Disqord.Bot.Hosting;
using Disqord.Gateway;
using Domain.Autocompletes.Default;
using Domain.Bot;
using Domain.Commands.Slash;
using Domain.Dispatcher.Default;
using Domain.Factories.Default;
using Domain.Options;
using Domain.Services.Default;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

IHost host = Host.CreateDefaultBuilder()
    .UseSerilog((ctx, logger) =>
    {
        logger.Filter
            .ByExcluding(e => e.Exception is Disqord.WebSocket.WebSocketClosedException);
        logger.ReadFrom
            .Configuration(ctx.Configuration);
    })
    .ConfigureServices((ctx, services) =>
    {
        services.AddDbContext<ApplicationContext>(dbCtx =>
            dbCtx.UseNpgsql(ctx.Configuration.GetConnectionString("DefaultConnection") ??
                            throw new InvalidOperationException("ConnectionString not found")));
        services.AddEntityFrameworkCoreRepositories<ApplicationContext>();

        services.AddTypedOptions(ctx.Configuration);

        services.AddCommandDispatcher();
        services.AddAutocompletes();
        services.AddFactories();
        services.AddServices();
        services.AddMemoryCache();
    })
    .ConfigureDiscordBot<MoBot>((ctx, bot) =>
    {
        var cfg = ctx.Configuration.GetRequiredSection("Discord").Get<DiscordOptions>();

        bot.Token = cfg?.Token;
        bot.OwnerIds = cfg?.OwnerSnowflakes;
        bot.Intents = GatewayIntents.Unprivileged | GatewayIntents.MessageContent;
        bot.ServiceAssemblies = new[] { typeof(MoBot).Assembly };
    })
    .Build();
    
await host.Services.GetRequiredService<ApplicationContext>().Database.MigrateAsync();
await host.RunAsync();