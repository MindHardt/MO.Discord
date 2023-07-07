using Data.Entities.Users;
using Disqord;
using Disqord.Bot.Commands.Application;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests;
using MediatR;
using Qmmands;
using Qommon;

namespace Domain.Bot.Commands;

[SlashGroup("владелец")]
public class OwnerCommandModule : DiscordApplicationModuleBase
{
    private readonly IMediator _mediator;
    private readonly IMappingProvider _mappingProvider;

    public OwnerCommandModule(IMediator mediator, IMappingProvider mappingProvider)
    {
        _mediator = mediator;
        _mappingProvider = mappingProvider;
    }

    [SlashCommand("подчинись")]
    [Description("Напоминает боту кто его тут главный. Только для владельцев бота.")]
    public async ValueTask<IResult> Obey()
    {
        var request = new EditUserDataRequest
        {
            UserId = Context.AuthorId,
            UserName = Context.Author.Name,
            AccessLevel = await Bot.IsOwnerAsync(Context.AuthorId) 
                ? AccessLevel.Admin 
                : Optional<AccessLevel>.Empty
        };
        var response = await _mediator.Send(request);
        var mapper = _mappingProvider.GetMessageMapper<UserData>();
        return Response(mapper.MapAs<LocalInteractionMessageResponse>(response.UserData));
    }
}