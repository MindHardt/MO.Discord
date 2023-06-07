using BotServices.MoBot;
using BotServices.Services.Core;
using BotServices.Services.Implementations;
using Data;
using Data.Repositories;
using Disqord;
using Disqord.Bot.Hosting;
using Disqord.Gateway;
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
        services.AddBotServices();
        services.AddMemoryCache();
    })
    .ConfigureDiscordBot<MoBot>((ctx, bot) =>
    {
        var token = ctx.Configuration["Discord:Token"];
        var ownerIds = ctx.Configuration.GetSection("Discord:OwnerIds").Get<ulong[]>();

        bot.Token = token;
        bot.OwnerIds = ownerIds?.Select(id => (Snowflake)id);
        bot.Intents = GatewayIntents.Unprivileged | GatewayIntents.MessageContent;
        bot.ServiceAssemblies = new[] { typeof(MoBot).Assembly };
    })
    .Build();
    
await host.Services.GetRequiredService<ApplicationContext>().Database.MigrateAsync();
await host.RunAsync();