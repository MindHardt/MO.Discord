using BotServices.Commands.Tags;
using BotServices.Entities.GuildData;
using BotServices.Factories.Core;
using BotServices.Services.Core;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Commands;

[SlashGroup("owner")]
[RequireBotOwner]
public class OwnerCommands : DiscordApplicationGuildModuleBase
{
    private readonly IGuildDataService _guildDataService;
    private readonly IDiscordResponseFactory _discordResponseFactory;
    private readonly ITagService _tagService;

    public OwnerCommands(
        IGuildDataService guildDataService, 
        IDiscordResponseFactory discordResponseFactory, 
        ITagService tagService)
    {
        _guildDataService = guildDataService;
        _discordResponseFactory = discordResponseFactory;
        _tagService = tagService;
    }
    
    [SlashCommand("inline-tags")]
    [Description("Switches inline tags for guild")]
    public async ValueTask<IResult> UpdateInlineTags(
        [Name("enabled"), Description("True, if inline tags must be searched, otherwise false")] 
        bool enabled)
    {
        var guildData = await _guildDataService.GetGuildDataAsync(Context.GuildId);
        guildData ??= new GuildData { GuildId = Context.GuildId };

        guildData.InlineTagsEnabled = enabled;

        await _guildDataService.SaveGuildDataAsync(guildData);

        string enabledStatus = enabled ? "🟢" : "⭕";
        var response = _discordResponseFactory.GetSuccessfulResponse($"Поиск тегов на сервере - {enabledStatus}");
        return Response(response);
    }
    
    [SlashCommand("tag-publicity")]
    [Description("Controls tags publicity")]
    public async ValueTask<IResult> TagPublicity(
        [Name("tag-name"), Description("Name of the edited tag.")]
        string tagName,
        [Name("public"), Description("True, if tag should be accessible in other guilds")]
        bool isPublic)
    {
        var tag = await _tagService.GetTagAsync(tagName, Context.GuildId);
        if (tag is null)
            return Results.Failure($"Тег `{tagName}` не найден");

        tag.GuildId = isPublic ? null : Context.GuildId;

        await _tagService.SaveTagAsync(tag, Context.AuthorId, await Bot.IsOwnerAsync(Context.AuthorId));

        var response = _discordResponseFactory.GetSuccessfulResponse();
        return Response(response);
    }
        
    [AutoComplete("tag-publicity")]
    public ValueTask TagNameAutocomplete(
        [Name("tag-name")] AutoComplete<string> tagName) =>
        TagsAutocompletes.TagName(tagName, Context.GuildId, _tagService);
}