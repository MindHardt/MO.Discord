using BotServices.Services.Core;
using Disqord;

namespace BotServices.Services.Implementations;

public class DefaultResponseService : IResponseService
{
    public LocalInteractionMessageResponse GetSuccessfulResponse() => new LocalInteractionMessageResponse()
        .WithEmbeds(new LocalEmbed()
            .WithTitle("✅ Успешно!"));

    public LocalInteractionMessageResponse GetSuccessfulResponse(string info) => new LocalInteractionMessageResponse()
        .WithEmbeds(new LocalEmbed()
            .WithTitle("✅ Успешно!")
            .WithDescription(info));
}