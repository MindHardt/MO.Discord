using BotServices.Entities.Tags;
using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Hosting;
using Disqord.Rest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BotServices.MoBot;

public class InlineTagNamesService : DiscordBotService
{

    protected override async ValueTask OnMessageReceived(BotMessageReceivedEventArgs e)
    {
        if (e.Member?.IsBot is not false) return;

        var guildDataService = Client.Services.GetRequiredService<IGuildDataService>();
        var tagService = Client.Services.GetRequiredService<ITagService>();

        if (e.GuildId is not null)
        {
            var guildData = await guildDataService.GetGuildDataAsync(e.GuildId.Value);
            if (guildData?.InlineTagsEnabled is not true) return;
        }
        
        var tagName = tagService.FindTagName(e.Message.Content);
        if (tagName is null) return;
        
        Logger.LogInformation("Captured inline tag {Name}", tagName);
        Tag? tag = await tagService.GetTagAsync(tagName, e.GuildId);

        if (tag is null)
        {
            Logger.LogInformation("Tag {Name} not found", tagName);
            await e.Message.AddReactionAsync(new LocalEmoji("🔍"));
            return;
        }

        var message = tagService.CreateMessage<LocalMessage>(tag)
            .WithReply(e.MessageId)
            .WithAllowedMentions(LocalAllowedMentions.None);
        await Bot.SendMessageAsync(e.ChannelId, message);
        Logger.LogInformation("Sent inline tag {Name}", tagName);
    }
}