using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Commands.Application;

namespace BotServices.Commands.Autocompletes;

public static class TagsAutocompletes
{
    public static async ValueTask TagName(AutoComplete<string> tagName, Snowflake? guildId, ITagService service)
    {
        if (tagName.IsFocused is false) return;
        
        var names = await service.GetTagNames(guildId, tagName.RawArgument);
        tagName.Choices.AddRange(names);
    }
}