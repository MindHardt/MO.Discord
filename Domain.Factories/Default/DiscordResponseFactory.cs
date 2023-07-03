using Disqord;
using Domain.Factories.Core;

namespace Domain.Factories.Default;

public class DiscordResponseFactory : IDiscordResponseFactory
{
    public TMessage GetSuccessfulMessageResponse<TMessage>(string? additionalInfo = null)
        where TMessage : LocalMessageBase, new()
        => new TMessage().AddEmbed(new LocalEmbed()
            .WithColor(Color.LimeGreen)
            .WithTitle("☑️ Успешно!")
            .WithDescription(additionalInfo ?? string.Empty));

    public TMessage GetFailedMessageResponse<TMessage>(string? additionalInfo = null) 
        where TMessage : LocalMessageBase, new()
        => new TMessage().AddEmbed(new LocalEmbed()
            .WithColor(Color.OrangeRed)
            .WithTitle("⚠️ Произошла ошибка!")
            .WithDescription(additionalInfo ?? string.Empty));
}