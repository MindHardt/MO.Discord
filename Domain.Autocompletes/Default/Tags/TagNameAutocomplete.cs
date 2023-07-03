using Disqord.Bot.Commands.Application;
using Domain.Autocompletes.Contexts.Tags;
using Domain.Autocompletes.Core;
using Domain.Services.Core.Tags;

namespace Domain.Autocompletes.Default.Tags;

public class TagNameAutocomplete : IAutocomplete<string, TagNameContext>
{
    private readonly ITagService _tagService;

    public TagNameAutocomplete(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async ValueTask Complete(AutoComplete<string> member, TagNameContext context)
    {
        if (member.IsFocused is false) return;
        
        var overviews = await _tagService.GetOverviews(member.RawArgument, context.GuildId);
        
        member.Choices.AddRange(overviews.Select(o => o.Name));
    }
}