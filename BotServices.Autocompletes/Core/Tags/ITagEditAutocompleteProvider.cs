using Disqord.Bot.Commands.Application;

namespace BotServices.Autocompletes.Core.Tags;

public interface ITagEditAutocompleteProvider :
    IAutocompleteProvider<string, IDiscordApplicationGuildCommandContext>
{
    
}