using Data.Entities;
using Data.Entities.Users;
using Disqord;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Disqord.Gateway;
using Domain.Bot.Checks;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests;
using MediatR;
using Qmmands;
using Qommon;

namespace Domain.Bot.Commands;

[SlashGroup("админ")]
[RequireAuthorPermissions(Permissions.Administrator)]
public class AdminCommandModule : DiscordApplicationGuildModuleBase
{
    private const string TrueString = "true";
    private const string FalseString = "false";

    private readonly IMediator _mediator;
    private readonly IMappingProvider _mappingProvider;

    public AdminCommandModule(
        IMediator mediator, 
        IMappingProvider mappingProvider)
    {
        _mediator = mediator;
        _mappingProvider = mappingProvider;
    }

    [SlashCommand("быстрые-теги")]
    [Description("Переключает быстрые теги, вызываемые внутри сообщений. Доступно только модераторам бота.")]
    [RequireAuthorAccessLevel(AccessLevel.Moderator)]
    public async ValueTask<IResult> SwitchInlineTags(
        [Name("состояние")]
        [Description("Состояние инлайн тегов")]
        [Choice("Включены", TrueString), Choice("Выключены", FalseString)]
        string state)
    {
        var request = new EditGuildDataRequest
        {
            GuildName = Optional.FromNullable(Context.Bot.GetGuild(Context.GuildId)?.Name),
            GuildId = Context.GuildId,
            InlineTagsEnabled = bool.Parse(state)
        };
        var response = await _mediator.Send(request);
        var mapper = _mappingProvider.GetMessageMapper<GuildData>();
        return Response(mapper.MapAs<LocalInteractionMessageResponse>(response.GuildData));
    }
    
    [SlashCommand("взрослый-контент")]
    [Description("Переключает взрослый контент, влияющий на работу части комманд. Доступно только модераторам бота.")]
    [RequireAuthorAccessLevel(AccessLevel.Moderator)]
    public async ValueTask<IResult> SwitchAdultAllowed(
        [Name("состояние")]
        [Description("Состояние инлайн тегов")]
        [Choice("Включен", TrueString), Choice("Выключен", FalseString)]
        string state)
    {
        var request = new EditGuildDataRequest
        {
            GuildName = Optional.FromNullable(Context.Bot.GetGuild(Context.GuildId)?.Name),
            GuildId = Context.GuildId,
            AdultAllowed = bool.Parse(state)
        };
        var response = await _mediator.Send(request);
        var mapper = _mappingProvider.GetMessageMapper<GuildData>();
        return Response(mapper.MapAs<LocalInteractionMessageResponse>(response.GuildData));
    }

    [SlashCommand("префикс-тегов")]
    [Description("Изменяет префикс быстрых тегов, вызываемых внутри сообщений.")]
    public async ValueTask<IResult> EditInlineTagsPrefix(
        [Name("префикс")]
        [Description("Текст который должен предшествовать названию тега. Желательно что-то короткое и простое.")]
        string prefix)
    {
        var request = new EditGuildDataRequest
        {
            GuildName = Optional.FromNullable(Context.Bot.GetGuild(Context.GuildId)?.Name),
            GuildId = Context.GuildId,
            InlineTagPrefix = prefix
        };
        var response = await _mediator.Send(request);
        var mapper = _mappingProvider.GetMessageMapper<GuildData>();
        return Response(mapper.MapAs<LocalInteractionMessageResponse>(response.GuildData));
    }
}