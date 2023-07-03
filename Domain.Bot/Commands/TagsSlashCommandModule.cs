using Disqord;
using Disqord.Bot.Commands.Application;
using Domain.Autocompletes.Contexts.Tags;
using Domain.Autocompletes.Core;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests.Tags;
using Qmmands;

namespace Domain.Bot.Commands;

[SlashGroup("тег")]
public class TagsSlashCommandModule : DiscordApplicationGuildModuleBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IAutocompleteProvider _autocompleteProvider;

    public TagsSlashCommandModule(
        ICommandDispatcher commandDispatcher, 
        IAutocompleteProvider autocompleteProvider)
    {
        _commandDispatcher = commandDispatcher;
        _autocompleteProvider = autocompleteProvider;
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
        => Response(await _commandDispatcher.ExecuteAs<LocalInteractionMessageResponse>(new GetTagRequest
        {
            GuildId = Context.GuildId,
            TagName = name
        }));

    [AutoComplete("отправить")]
    public ValueTask TagNameAutocomplete(
        [Name("название")] AutoComplete<string> tagName)
        => _autocompleteProvider.GetTagName().CompleteAsync(tagName, new TagNameContext
        {
            GuildId = Context.GuildId,
        });
}