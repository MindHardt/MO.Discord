using Disqord;
using Domain.Factories.Abstractions;

namespace Domain.Factories.Default;

public class DiscordResponseFactory : IDiscordResponseFactory
{
    public TMessage GetSuccessfulMessageResponse<TMessage>(string? additionalInfo = null)
        where TMessage : LocalMessageBase, new()
        => new TMessage().AddEmbed(new LocalEmbed()
            .WithTitle("☑️ Успешно!").WithDescription(additionalInfo ?? string.Empty));
}