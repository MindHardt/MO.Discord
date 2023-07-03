using Disqord;
using Disqord.Bot.Hosting;
using Disqord.Rest;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests.Tags;
using Domain.Services.Core;
using Domain.Services.Core.Tags;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Domain.Bot.Services;

public class InlineTagDiscordBotService : DiscordBotService
{
    private readonly ILogger<InlineTagDiscordBotService> _logger;

    public InlineTagDiscordBotService(ILogger<InlineTagDiscordBotService> logger)
    {
        _logger = logger;
    }

    protected override async ValueTask OnMessageReceived(BotMessageReceivedEventArgs e)
    {
        if (e.GuildId is null || e.Member?.IsBot is true) return;
        
        _logger.LogInformation("Received message {MessageId} in guild {GuildId}", 
            e.MessageId, e.GuildId.Value);

        using var scope = Bot.Services.CreateScope();
        
        var guildService = scope.ServiceProvider.GetRequiredService<IGuildService>();
        var guildData = await guildService.GetOrCreateAsync(e.GuildId.Value);

        if (guildData.InlineTagsEnabled is false) return;
        
        _logger.LogInformation("Attempting to capture tag in message {MessageId} in guild {GuildId}", 
            e.MessageId, e.GuildId.Value);

        var tagNameService = scope.ServiceProvider.GetRequiredService<ITagNameService>();
        var foundTagName = tagNameService.FindTagName(e.Message.Content, guildData.InlineTagsPrefix);

        if (foundTagName is null) return;
        
        _logger.LogInformation("Captured tag name {Name} in message {MessageId} in guild {GuildId}", 
            foundTagName, e.MessageId, e.GuildId.Value);
        
        var dispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();
        var request = new GetTagRequest
        {
            GuildId = e.GuildId.Value,
            TagName = foundTagName
        };
        var message = await dispatcher.ExecuteAs<LocalMessage>(request);
        
        message
            .WithReply(e.MessageId)
            .WithAllowedMentions(LocalAllowedMentions.None);

        await Bot.SendMessageAsync(e.ChannelId, message);
    }
}