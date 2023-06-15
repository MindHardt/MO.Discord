using BotServices.Autocompletes.Core.Tags;
using BotServices.CQRS.Dispatcher.Core;
using BotServices.CQRS.Requests.Tags;
using BotServices.Entities.Tags;
using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Commands.Tags.Slash;

[SlashGroup("тег")]
public partial class TagsCommandModule : DiscordApplicationGuildModuleBase
{
    private readonly ITagViewAutocompleteProvider _viewAutocompleteProvider;
    private readonly IDiscordCommandDispatcher _discordCommandDispatcher;
    private readonly ITagEditAutocompleteProvider _editAutocompleteProvider;
    
    public TagsCommandModule(
        IDiscordCommandDispatcher discordCommandDispatcher, 
        ITagViewAutocompleteProvider viewAutocompleteProvider, 
        ITagEditAutocompleteProvider editAutocompleteProvider)
    {
        _discordCommandDispatcher = discordCommandDispatcher;
        _viewAutocompleteProvider = viewAutocompleteProvider;
        _editAutocompleteProvider = editAutocompleteProvider;
    }

    [SlashCommand("поиск")]
    [Description("Находит все теги с названиями похожими на запрос")]
    public async ValueTask<IResult> TagSearch(
        [Name("запрос"), Description("Часть названия тега по которой будем искать")]
        string prompt)
        => await _discordCommandDispatcher.DispatchAsync(new TagSearchRequest
        {
            Context = Context,
            Prompt = prompt,
        });

    [SlashCommand("отправить")]
    [Description("Отправляет сохраненный тег")]
    public async ValueTask<IResult> TagSend(
        [Name("название"), Description("Название отправляемого тега"), Maximum(Constants.MaxNameLength)]
        string name)
        => await _discordCommandDispatcher.DispatchAsync(new GetTagRequest
        {
            Context = Context,
            TagName = name
        });

    [SlashCommand("создать")]
    [Description("Создает тег из предоставленного текста")]
    public async ValueTask<IResult> TagCreate(
        [Name("название"), Description("Название будущего тега"), Maximum(Constants.MaxNameLength)]
        string name,
        [Name("содержимое"), Description("Текст будущего тега"), Maximum(Constants.MaxContentLength)]
        string text)
        => await _discordCommandDispatcher.DispatchAsync(new CreateTagRequest
        {
            Context = Context,
            Name = name,
            Text = text
        });

    [SlashCommand("удалить")]
    [Description("Удаляет выбранный тег")]
    public async ValueTask<IResult> TagDelete(
        [Name("название"), Description("Название тега"), Maximum(Constants.MaxNameLength)]
        string name)
        => await _discordCommandDispatcher.DispatchAsync(new DeleteTagRequest
        {
            Context = Context,
            TagName = name
        });

    [AutoComplete("отправить")]
    public ValueTask TagViewAutocomplete(
        [Name("название")] AutoComplete<string> tagName) =>
        _viewAutocompleteProvider
            .GetAutocomplete()
            .CompleteAsync(tagName, Context);
    
    [AutoComplete("удалить")]
    public ValueTask TagDeleteAutocomplete(
        [Name("название")] AutoComplete<string> tagName) =>
        _editAutocompleteProvider.GetAutocomplete().CompleteAsync(tagName, Context);
}