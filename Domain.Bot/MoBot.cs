using Disqord;
using Disqord.Bot;
using Disqord.Bot.Commands.Application;
using Domain.Commands.Slash;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domain.Bot;

public class MoBot : DiscordBot
{
    public MoBot(
        IOptions<DiscordBotConfiguration> options, 
        ILogger<MoBot> logger, 
        IServiceProvider services, 
        DiscordClient client) 
        : base(options, logger, services, client)
    { }
    
    protected override ValueTask OnInitialize(CancellationToken cancellationToken)
    {
        Commands.AddModules(typeof(TagsSlashCommandModule).Assembly);

        Logger.LogInformation("Recognizing {Count} owners", OwnerIds.Count);

        var commandModules = Commands.EnumerateApplicationModules().ToArray();
        
        Logger.LogInformation("Registered {Modules} command modules with {Commands} commands", 
            commandModules.Length, commandModules.Sum(c => c.Commands.Count));

        return base.OnInitialize(cancellationToken);
    }
}