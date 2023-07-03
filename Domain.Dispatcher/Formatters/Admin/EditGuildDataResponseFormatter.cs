using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Responses.Admin;
using Domain.Factories.Core;

namespace Domain.Dispatcher.Formatters.Admin;

public class EditGuildDataResponseFormatter : IFormatter<EditGuildDataResponse, LocalInteractionMessageResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public EditGuildDataResponseFormatter(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public LocalInteractionMessageResponse Format(EditGuildDataResponse source)
    {
        var embedFields = new[]
        {
            new LocalEmbedField()
                .WithName("Быстрые теги🔗")
                .WithValue(Markdown.Code(_discordResponseFactory.GetBoolean(source.GuildData.InlineTagsEnabled))),
            new LocalEmbedField()
                .WithName("Префикс тегов")
                .WithValue(Markdown.Code(source.GuildData.InlineTagsPrefix))
        };
        var title = $"Сервер {source.GuildData.GuildId}";

        return _discordResponseFactory.GetSuccessfulMessageResponse<LocalInteractionMessageResponse>()
            .AddEmbed(new LocalEmbed()
                .WithTitle(title)
                .WithFields(embedFields));
    }
}