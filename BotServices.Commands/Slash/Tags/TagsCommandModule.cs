using BotServices.Commands.Autocompletes;
using BotServices.Entities.Tags;
using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Commands.Slash.Tags;

[SlashGroup("tag")]
public partial class TagsCommandModule : DiscordApplicationGuildModuleBase
{
    private readonly ITagService _tagService;
    private readonly IResponseService _responseService;

    public TagsCommandModule(
        ITagService tagService,
        IResponseService responseService)
    {
        _tagService = tagService;
        _responseService = responseService;
    }

    [SlashCommand("search")]
    [Description("Находит все теги с названиями похожими на запрос")]
    public async ValueTask<IResult> TagSearch(
        [Name("prompt"), Description("Часть названия тега по которой будем искать")]
        string prompt)
    {
        var tags = await _tagService.GetTagsAsync(Context.GuildId, prompt);

        var lines = tags
            .Select(_tagService.CreateOverview)
            .Select((l, i) => $"{i + 1}. {l}");
        var descriptionRaw = string.Join('\n', lines);

        const int maxDescriptionLength = Discord.Limits.Message.Embed.MaxDescriptionLength - 8;

        var descriptionContent = $"```\n{descriptionRaw[..Math.Min(descriptionRaw.Length, maxDescriptionLength)]}\n```";

        var response = new LocalInteractionMessageResponse()
            .WithEmbeds(new LocalEmbed()
                .WithDescription(descriptionContent)
                .WithTitle($"Теги, соответствующие запросу `{prompt}`"));
        return Response(response);
    }

    [SlashCommand("create")]
    [Description("Создает тег из предоставленного текста")]
    public async ValueTask<IResult> TagCreate(
        [Name("name"), Description("Название будущего тега"), Maximum(Constants.MaxNameLength)]
        string name,
        [Name("content"), Description("Текст будущего тега"), Maximum(Constants.MaxContentLength)]
        string text)
    {
        if (_tagService.GetTagNameRegex().IsMatch(name) is false)
            return Results.Failure("Это имя недопустимо для тега.");
        
        Snowflake ownerId = Context.AuthorId;
        Snowflake guildId = Context.GuildId;
        var tag = _tagService.CreateTagMessage(name, text, ownerId, guildId);

        await _tagService.SaveTagAsync(tag, ownerId, await Bot.IsOwnerAsync(ownerId));

        var response = _responseService.GetSuccessfulResponse();
        return Response(response);
    }

    [SlashCommand("send")]
    [Description("Отправляет сохраненный тег")]
    public async ValueTask<IResult> TagSend(
        [Name("name"), Description("Название отправляемого тега"), Maximum(Constants.MaxNameLength)]
        string name)
    {
        var guildId = Context.GuildId;
        Tag? tag = await _tagService.GetTagAsync(name, guildId);

        ArgumentNullException.ThrowIfNull(tag, nameof(tag));

        var response = _tagService.CreateMessage<LocalInteractionMessageResponse>(tag);
        return Response(response);
    }

    [AutoComplete("send")]
    public ValueTask TagNameAutocomplete(
        [Name("name")] AutoComplete<string> tagName) =>
        TagsAutocompletes.TagName(tagName, Context.GuildId, _tagService);
}