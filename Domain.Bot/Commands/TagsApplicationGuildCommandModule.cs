using Disqord;
using Disqord.Bot.Commands.Application;
using Domain.Commands.Dispatcher;
using Domain.Commands.Requests.Tags;
using Qmmands;

namespace Domain.Bot.Commands;

[SlashGroup("тег")]
public class TagsApplicationGuildCommandModule : DiscordApplicationGuildModuleBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public TagsApplicationGuildCommandModule(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [SlashCommand("создать")]
    [Description("Создает тег из набранного текста")]
    public async ValueTask<IResult> CreateTag(
        [Name("название")]
        [Description("Название будущего тега")]
        string name,
        [Name("текст")]
        [Description("Текст будущего тега")]
        string text)
        => Response(await _commandDispatcher.ExecuteAs<LocalInteractionMessageResponse>(new CreateMessageTagRequest
        {
            TagText = text,
            TagName = name,
            AuthorId = Context.AuthorId,
            GuildId = Context.GuildId
        }));
    
    [SlashCommand("отправить")]
    [Description("Отправляет выбранный тег")]
    public async ValueTask<IResult> SendTag(
        [Name("название")]
        [Description("Название тега")]
        string name)
        => Response(await _commandDispatcher.ExecuteAs<LocalInteractionMessageResponse>(new GetTagRequest()
        {
            GuildId = Context.GuildId,
            TagName = name
        }));
}