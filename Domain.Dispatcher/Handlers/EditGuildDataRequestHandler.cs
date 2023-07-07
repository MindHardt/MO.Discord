using Domain.Dispatcher.Requests;
using Domain.Dispatcher.Responses;
using Domain.Services.Core;
using MediatR;
using Qommon;

namespace Domain.Dispatcher.Handlers;

public class EditGuildDataRequestHandler : IRequestHandler<EditGuildDataRequest, EditGuildDataResponse>
{
    private readonly IGuildService _guildService;

    public EditGuildDataRequestHandler(IGuildService guildService)
    {
        _guildService = guildService;
    }

    public async Task<EditGuildDataResponse> Handle(EditGuildDataRequest request, CancellationToken cancellationToken)
    {
        var guildData = await _guildService.GetOrCreateAsync(request.GuildId);

        guildData.InlineTagsEnabled = request.InlineTagsEnabled.GetValueOrDefault(guildData.InlineTagsEnabled);
        guildData.InlineTagsPrefix = request.InlineTagPrefix.GetValueOrDefault(guildData.InlineTagsPrefix);
        guildData.GuildName = request.GuildName.GetValueOrDefault(guildData.GuildName);
        guildData.AdultAllowed = request.AdultAllowed.GetValueOrDefault(guildData.AdultAllowed);

        guildData = await _guildService.UpdateGuildAsync(guildData);

        return new EditGuildDataResponse
        {
            GuildData = guildData
        };
    }
}