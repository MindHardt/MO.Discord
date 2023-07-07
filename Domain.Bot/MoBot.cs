using Disqord;
using Disqord.Bot;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Domain.Bot.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qmmands;

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

    protected override string? FormatFailureReason(IDiscordCommandContext context, IResult result) => result switch
    {
        CommandNotFoundResult => BotResources.Failure_CommandNotFound,
        
        TypeParseFailedResult res => string.Format(
            BotResources.Failure_TypeParseFailed, 
            res.Parameter.Name,
            Markdown.CodeBlock(res.Value.ToString())),
        
        ChecksFailedResult res => string.Format(
            BotResources.Failure_ChecksFailed,
            Markdown.CodeBlock(string.Join('\n', res.FailedChecks
                .Select(x => x.Value.FailureReason)))),
        
        ParameterChecksFailedResult res => string.Format(
            BotResources.Failure_ParameterChecksFailed,
            res.Parameter.Name,
            Markdown.CodeBlock(string.Join('\n', res.FailedChecks
                .Select(x => x.Value.FailureReason)))),
        
        ExceptionResult res => string.Format(
            BotResources.Failure_Exception,
            res.Exception.Message),
        
        _ => result.FailureReason
    };
}