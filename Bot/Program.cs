using Data.EFCore;
using Data.EFCore.Repositories;
using Disqord;
using Disqord.Bot.Hosting;
using Disqord.Gateway;
using Disqord.Http;
using Domain.Autocompletes.Default;
using Domain.Bot;
using Domain.Dispatcher.Handlers.Tags;
using Domain.Dispatcher.Mappers.Common;
using Domain.Factories.Default;
using Domain.Options;
using Domain.Services.Default;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

void ConfigureSerilog(HostBuilderContext ctx, LoggerConfiguration logger)
{
    logger.Filter.ByExcluding(e => e.Exception is Disqord.WebSocket.WebSocketClosedException);
    logger.ReadFrom.Configuration(ctx.Configuration);
}

void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
{
    services.AddDbContext<ApplicationContext>(dbCtx => 
        dbCtx.UseNpgsql(ctx.Configuration.GetConnectionString("DefaultConnection") 
                        ?? throw new InvalidOperationException("ConnectionString not found")));
    services.AddEntityFrameworkCoreRepositories<ApplicationContext>();

    services.Configure<DiscordOptions>(ctx.Configuration.GetSection("Discord").Bind);
    services.Configure<CacheOptions>(ctx.Configuration.GetSection("Cache").Bind);

    services.AddMediatR(options =>
    {
        options.RegisterServicesFromAssemblyContaining<GetTagRequestHandler>();
    });
    services.AddMappers();
    services.AddAutocompletes();
    services.AddFactories();
    services.AddServices();
    services.AddMemoryCache();

    services.AddScoped<HttpClient>();
}

void ConfigureDiscordBot(HostBuilderContext ctx, DiscordBotHostingContext bot)
{
    var cfg = ctx.Configuration.GetRequiredSection("Discord").Get<DiscordOptions>();

    bot.Token = cfg?.Token;
    bot.OwnerIds = cfg?.OwnerSnowflakes;
    bot.Intents = GatewayIntents.Unprivileged | GatewayIntents.MessageContent;
    bot.ServiceAssemblies = new[] { typeof(MoBot).Assembly };
}

IHost host = Host.CreateDefaultBuilder()
    .UseSerilog(ConfigureSerilog)
    .ConfigureServices(ConfigureServices)
    .ConfigureDiscordBot<MoBot>(ConfigureDiscordBot)
    .Build();
    
await host.Services.GetRequiredService<ApplicationContext>().Database.MigrateAsync();
await host.RunAsync();
