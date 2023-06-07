using BotServices.Entities.Tags;
using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Hosting;
using Disqord.Rest;
using Microsoft.Extensions.Logging;

namespace BotServices.MoBot;

public class InlineTagNamesService : DiscordBotService
{
    private ITagService _tagService;

    public InlineTagNamesService(ITagService tagService)
    {
        _tagService = tagService;
    }

    protected override async ValueTask OnMessageReceived(BotMessageReceivedEventArgs e)
    {
        var match = _tagService.GetTagNameRegex().Match(e.Message.Content);
        if (match.Success is false) return;

        var tagName = match.Groups["NAME"].Value;
        Logger.LogInformation("Captured inline tag {Name}", tagName);
        Tag? tag = await _tagService.GetTagAsync(tagName, e.GuildId);

        if (tag is null)
        {
            Logger.LogInformation("Tag {Name} not found", tagName);
            await e.Message.AddReactionAsync(new LocalEmoji("❌"));
            return;
        }

        var message = _tagService.CreateMessage<LocalMessage>(tag)
            .WithReply(e.MessageId);
        await Bot.SendMessageAsync(e.ChannelId, message);
        Logger.LogInformation("Sent inline tag {Name}", tagName);
    }
}