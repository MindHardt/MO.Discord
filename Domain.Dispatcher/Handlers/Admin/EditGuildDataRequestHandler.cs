using Domain.Dispatcher.Requests.Admin;
using Domain.Dispatcher.Responses.Admin;
using Domain.Services.Core;
using MediatR;

namespace Domain.Dispatcher.Handlers.Admin;

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
        
        guildData.InlineTagsEnabled = request.InlineTagsEnabled ?? guildData.InlineTagsEnabled;
        guildData.InlineTagsPrefix = request.InlineTagPrefix ?? guildData.InlineTagsPrefix;

        guildData = await _guildService.UpdateGuildAsync(guildData);

        return new EditGuildDataResponse
        {
            GuildData = guildData
        };
    }
}