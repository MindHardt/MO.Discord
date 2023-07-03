using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Responses.Owner;
using Domain.Factories.Core;
using Domain.Services.Core;

namespace Domain.Dispatcher.Formatters.Owner;

public class ObeyResponseFormatter : IFormatter<ObeyResponse, LocalInteractionMessageResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;
    private readonly IUserService _userService;

    public ObeyResponseFormatter(
        IDiscordResponseFactory discordResponseFactory, 
        IUserService userService)
    {
        _discordResponseFactory = discordResponseFactory;
        _userService = userService;
    }

    public LocalInteractionMessageResponse Format(ObeyResponse source)
    {
        var embedFields = new[]
        {
            new LocalEmbedField()
                .WithName("Уровень доступа")
                .WithValue($"{(int)source.UpdatedUser.AccessLevel}-{source.UpdatedUser.AccessLevel}"),
            new LocalEmbedField()
                .WithName("Личный лимит тегов")
                .WithValue(_discordResponseFactory.GetBoolean(source.UpdatedUser.CustomTagLimit is not null)),
            new LocalEmbedField()
                .WithName("Лимит тегов")
                .WithValue(_userService.GetTagLimit(source.UpdatedUser)?.ToString() ?? "-")
        };
        var title = $"Пользователь {source.UpdatedUser.UserId}";

        return _discordResponseFactory.GetSuccessfulMessageResponse<LocalInteractionMessageResponse>()
            .AddEmbed(new LocalEmbed()
                .WithTitle(title)
                .WithFields(embedFields));
    }
}