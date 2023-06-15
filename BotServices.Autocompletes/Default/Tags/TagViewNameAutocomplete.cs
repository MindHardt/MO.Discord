using BotServices.Autocompletes.Core;
using BotServices.Services.Core;
using Disqord.Bot.Commands.Application;

namespace BotServices.Autocompletes.Default.Tags;

public class TagViewNameAutocomplete : IAutocomplete<string, IDiscordApplicationGuildCommandContext>
{
    private readonly ITagService _tagService;

    public TagViewNameAutocomplete(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async ValueTask CompleteAsync(AutoComplete<string> parameter, IDiscordApplicationGuildCommandContext context)
    {
        if (parameter.IsFocused is false) return;
        
        var names = await _tagService.GetTagNames(context.GuildId, parameter.RawArgument);
        parameter.Choices.AddRange(names);
    }
}