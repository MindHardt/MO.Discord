using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Autocompletes.Core;

public interface IAutocomplete<T, in TContext> 
    where T : notnull
    where TContext : ICommandContext
{
    public ValueTask CompleteAsync(AutoComplete<T> parameter, TContext context);
}