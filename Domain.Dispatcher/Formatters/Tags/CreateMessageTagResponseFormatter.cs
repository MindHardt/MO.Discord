using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Responses.Tags;
using Domain.Factories.Core;

namespace Domain.Dispatcher.Formatters.Tags;

public class CreateMessageTagResponseFormatter : IFormatter<CreateMessageTagResponse, LocalInteractionMessageResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public CreateMessageTagResponseFormatter(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public LocalInteractionMessageResponse Format(CreateMessageTagResponse source)
        => _discordResponseFactory
            .GetSuccessfulMessageResponse<LocalInteractionMessageResponse>(
                $"Успешно создал тег `{source.CreatedTag.Name}`");
}