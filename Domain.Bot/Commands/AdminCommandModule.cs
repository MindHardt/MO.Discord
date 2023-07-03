using Data.Entities.Users;
using Disqord;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Domain.Bot.Checks;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests.Admin;
using Qmmands;

namespace Domain.Bot.Commands;

[SlashGroup("админ")]
[RequireAuthorPermissions(Permissions.Administrator)]
public class AdminCommandModule : DiscordApplicationGuildModuleBase
{
    private const string TrueString = "true";
    private const string FalseString = "false";

    private readonly ICommandDispatcher _commandDispatcher;

    public AdminCommandModule(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [SlashCommand("быстрые-теги")]
    [Description("Переключает быстрые теги, вызываемые внутри сообщений. Доступно только модераторам бота.")]
    [RequireAuthorAccessLevel(UserAccessLevel.Moderator)]
    public async ValueTask<IResult> SwitchInlineTags(
        [Name("состояние")]
        [Description("Состояние инлайн тегов")]
        [Choice("Включены", TrueString), Choice("Выключены", FalseString)]
        string state)
        => Response(await _commandDispatcher.ExecuteAs<LocalInteractionMessageResponse>(new EditGuildDataRequest
        {
            GuildId = Context.GuildId,
            InlineTagsEnabled = bool.Parse(state)
        }));
    
    [SlashCommand("префикс-тегов")]
    [Description("Изменяет префикс быстрых тегов, вызываемых внутри сообщений.")]
    public async ValueTask<IResult> EditInlineTagsPrefix(
        [Name("префикс")]
        [Description("Текст который должен предшествовать названию тега. Желательно что-то короткое и простое.")]
        string prefix)
        => Response(await _commandDispatcher.ExecuteAs<LocalInteractionMessageResponse>(new EditGuildDataRequest
        {
            GuildId = Context.GuildId,
            InlineTagPrefix = prefix
        }));
}