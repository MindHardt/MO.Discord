using Disqord;
using Disqord.Bot.Commands.Application;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests.Owner;
using Qmmands;

namespace Domain.Bot.Commands;

[SlashGroup("владелец")]
public class OwnerCommandModule : DiscordApplicationModuleBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public OwnerCommandModule(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [SlashCommand("подчинись")]
    [Description("Напоминает боту кто его тут главный. Только для владельцев бота.")]
    public async ValueTask<IResult> Obey()
        => Response(await _commandDispatcher.ExecuteAs<LocalInteractionMessageResponse>(new ObeyRequest
        {
            UserId = Context.AuthorId,
            IsOwner = await Bot.IsOwnerAsync(Context.AuthorId)
        }));
}