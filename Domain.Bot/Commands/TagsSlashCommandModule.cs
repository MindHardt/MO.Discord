using Data.Entities.Tags;
using Disqord;
using Disqord.Bot.Commands.Application;
using Domain.Autocompletes.Contexts.Tags;
using Domain.Autocompletes.Core;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests.Tags;
using MediatR;
using Qmmands;

namespace Domain.Bot.Commands;

[SlashGroup("тег")]
public class TagsSlashCommandModule : DiscordApplicationGuildModuleBase
{
    private readonly IMediator _mediator;
    private readonly IAutocompleteProvider _autocompleteProvider;
    private readonly IMappingProvider _mappingProvider;

    public TagsSlashCommandModule(
        IMediator mediator, 
        IAutocompleteProvider autocompleteProvider, 
        IMappingProvider mappingProvider)
    {
        _mediator = mediator;
        _autocompleteProvider = autocompleteProvider;
        _mappingProvider = mappingProvider;
    }

    [SlashCommand("создать")]
    [Description("Создает тег из набранного текста")]
    public async ValueTask<IResult> CreateTag(
        [Name("название")] [Description("Название будущего тега")]
        string name,
        [Name("текст")] [Description("Текст будущего тега")]
        string text)
    {
        var request = new CreateMessageTagRequest
        {
            TagText = text,
            TagName = name,
            AuthorId = Context.AuthorId,
            GuildId = Context.GuildId
        };
        var response = await _mediator.Send(request);
        var mapper = _mappingProvider.GetMessageMapper<TagOverview>();
        return Response(mapper.MapAs<LocalInteractionMessageResponse>(TagOverview.Create(response.CreatedTag)));
    }

    [SlashCommand("отправить")]
    [Description("Отправляет выбранный тег")]
    public async ValueTask<IResult> SendTag(
        [Name("название")] [Description("Название тега")]
        string name)
    {
        var request = new GetTagRequest
        {
            GuildId = Context.GuildId,
            TagName = name
        };
        var response = await _mediator.Send(request);
        var mapper = _mappingProvider.GetMessageMapper<Tag>();
        return Response(mapper.MapAs<LocalInteractionMessageResponse>(response.FoundTag));
    }

    [AutoComplete("отправить")]
    public ValueTask TagNameAutocomplete(
        [Name("название")] AutoComplete<string> tagName)
        => _autocompleteProvider.GetTagName().CompleteAsync(tagName, new TagNameContext
        {
            GuildId = Context.GuildId,
        });
}