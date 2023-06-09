using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Commands.Application;

namespace BotServices.Commands.Tags;

public static class TagsAutocompletes
{
    public static async ValueTask TagName(AutoComplete<string> tagName, Snowflake? guildId, ITagService service, Snowflake? editorId = null)
    {
        if (tagName.IsFocused is false) return;
        
        var names = await service.GetTagNames(guildId, tagName.RawArgument, editorId);
        tagName.Choices.AddRange(names);
    }
}