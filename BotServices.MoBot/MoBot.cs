using BotServices.Commands.Tags.Slash;
using Disqord;
using Disqord.Bot;
using Disqord.Bot.Commands.Application;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BotServices.MoBot;

public partial class MoBot : DiscordBot
{
    public MoBot(
        IOptions<DiscordBotConfiguration> options, 
        ILogger<MoBot> logger, 
        IServiceProvider services, 
        DiscordClient client) 
        : base(options, logger, services, client)
    {
    }
    
    protected override ValueTask OnInitialize(CancellationToken cancellationToken)
    {
        Commands.AddModules(typeof(TagsCommandModule).Assembly);

        Logger.LogInformation("Recognizing {Count} owners", OwnerIds.Count);
        Logger.LogInformation("Listening to {Count} commands", Commands
            .EnumerateApplicationModules()
            .SelectMany(c => c.Commands)
            .Count());

        return base.OnInitialize(cancellationToken);
    }
}