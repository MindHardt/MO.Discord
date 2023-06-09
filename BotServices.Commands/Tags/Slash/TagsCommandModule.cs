using BotServices.Entities.Tags;
using BotServices.Factories.Core;
using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Commands.Tags.Slash;

[SlashGroup("тег")]
public partial class TagsCommandModule : DiscordApplicationGuildModuleBase
{
    private readonly ITagService _tagService;
    private readonly IDiscordResponseFactory _discordResponseFactory;
    private readonly ITagFactory _tagFactory;

    public TagsCommandModule(
        ITagService tagService,
        IDiscordResponseFactory discordResponseFactory, 
        ITagFactory tagFactory)
    {
        _tagService = tagService;
        _discordResponseFactory = discordResponseFactory;
        _tagFactory = tagFactory;
    }

    [SlashCommand("поиск")]
    [Description("Находит все теги с названиями похожими на запрос")]
    public async ValueTask<IResult> TagSearch(
        [Name("запрос"), Description("Часть названия тега по которой будем искать")]
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
    
    
    [SlashCommand("отправить")]
    [Description("Отправляет сохраненный тег")]
    public async ValueTask<IResult> TagSend(
        [Name("название"), Description("Название отправляемого тега"), Maximum(Constants.MaxNameLength)]
        string name)
    {
        var guildId = Context.GuildId;
        var tag = await _tagService.GetTagAsync(name, guildId);

        if (tag is null)
            throw new InvalidOperationException($"Тег `{name}` не найден");

        var response = _tagService.CreateMessage<LocalInteractionMessageResponse>(tag);
        return Response(response);
    }

    [SlashCommand("создать")]
    [Description("Создает тег из предоставленного текста")]
    public async ValueTask<IResult> TagCreate(
        [Name("название"), Description("Название будущего тега"), Maximum(Constants.MaxNameLength)]
        string name,
        [Name("содержимое"), Description("Текст будущего тега"), Maximum(Constants.MaxContentLength)]
        string text)
    {
        Snowflake ownerId = Context.AuthorId;
        Snowflake guildId = Context.GuildId;
        var tag = _tagFactory.CreateTagMessage(name, text, ownerId, guildId);

        await _tagService.SaveTagAsync(tag, ownerId, await Bot.IsOwnerAsync(ownerId));

        var response = _discordResponseFactory.GetSuccessfulResponse();
        return Response(response);
    }

    [SlashCommand("удалить")] [Description("Удаляет выбранный тег")]
    public async ValueTask<IResult> TagDelete(
        [Name("название"), Description("Название тега"), Maximum(Constants.MaxNameLength)]
        string name)
    {
        await _tagService.DeleteTagAsync(name, Context.AuthorId);
        
        var response = _discordResponseFactory.GetSuccessfulResponse();
        return Response(response);
    }

    [AutoComplete("отправить")]
    [AutoComplete("удалить")]
    public ValueTask TagNameAutocomplete(
        [Name("название")] AutoComplete<string> tagName) =>
        TagsAutocompletes.TagName(tagName, Context.GuildId, _tagService);
}