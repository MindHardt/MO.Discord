using BotServices.Autocompletes.Core.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.Autocompletes.Default.Tags;

public class TagEditNameAutocompleteProvider :
    DefaultAutocompleteProviderBase<string, IDiscordApplicationGuildCommandContext, TagEditNameAutocomplete>,
    ITagEditAutocompleteProvider
{
    public TagEditNameAutocompleteProvider(IServiceProvider services) : base(services)
    {
    }
}