using BotServices.Autocompletes.Core.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.Autocompletes.Default.Tags;

public class TagViewNameAutocompleteProvider : 
    DefaultAutocompleteProviderBase<string, IDiscordApplicationGuildCommandContext, TagViewNameAutocomplete>,
    ITagViewAutocompleteProvider
{
    public TagViewNameAutocompleteProvider(IServiceProvider services) : base(services)
    {
    }
}