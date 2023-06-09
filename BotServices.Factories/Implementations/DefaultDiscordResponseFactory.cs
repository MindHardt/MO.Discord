using BotServices.Entities.Tags;
using BotServices.Factories.Core;
using Disqord;

namespace BotServices.Factories.Implementations;

[Factory]
public class DefaultDiscordResponseFactory : IDiscordResponseFactory
{
    public LocalInteractionMessageResponse GetSuccessfulResponse(string? info = null) => new LocalInteractionMessageResponse()
        .WithEmbeds(new LocalEmbed()
            .WithColor(Color.Lime)
            .WithTitle("✅ Успешно!")
            .WithDescription(info ?? string.Empty));

    public LocalInteractionMessageResponse GetFailedResponse(string? info = null) => new LocalInteractionMessageResponse()
        .WithEmbeds(new LocalEmbed()
            .WithColor(Color.Red)
            .WithTitle("❌ Произошла ошибка")
            .WithDescription(info ?? string.Empty));

    public LocalInteractionModalResponse GetTagCreationModal(string? customId = null) => new LocalInteractionModalResponse()
        .WithTitle("Создание тега")
        .WithCustomId(customId ?? Guid.NewGuid().ToString())
        .WithComponents(LocalComponent.Row(new LocalTextInputComponent()
                .WithLabel("Введите название тега")
                .WithCustomId(customId ?? Guid.NewGuid().ToString())
                .WithMaximumInputLength(Constants.MaxNameLength)
                .WithStyle(TextInputComponentStyle.Short)));
}