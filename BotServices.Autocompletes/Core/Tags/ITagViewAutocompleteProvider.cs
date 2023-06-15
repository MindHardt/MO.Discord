using Disqord.Bot.Commands.Application;

namespace BotServices.Autocompletes.Core.Tags;

public interface ITagViewAutocompleteProvider : 
    IAutocompleteProvider<string, IDiscordApplicationGuildCommandContext>
{
    
}