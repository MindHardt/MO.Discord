using BotServices.Commands.Autocompletes;
using BotServices.Entities.Tags;
using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Commands.Slash.Tags;

public partial class TagsCommandModule
{
    [SlashGroup("alias")]
    public class TagAliasCommandModule : DiscordApplicationGuildModuleBase
    {
        private readonly ITagService _tagService;
        private readonly IResponseService _responseService;

        public TagAliasCommandModule(
            ITagService tagService, 
            IResponseService responseService)
        {
            _tagService = tagService;
            _responseService = responseService;
        }


        [SlashCommand("create")]
        public async ValueTask<IResult> Create(
            [Name("original"), Description("Название исходного тега")]
            string originalName,
            [Name("alias"), Description("Новое название тега")]
            string newName)
        {
            Snowflake guildId = Context.GuildId;
            Tag? tag = await _tagService.GetTagAsync(originalName, guildId);

            if (tag is null) return Results.Failure($"Tag '{originalName}' not found");

            TagMessage tagMessage = tag.GetTagMessage();
            Snowflake ownerId = Context.AuthorId;
            TagAlias alias = _tagService.CreateTagAlias(tagMessage, newName, ownerId, guildId);

            await _tagService.SaveTagAsync(alias, ownerId, await Bot.IsOwnerAsync(ownerId));

            var message = $"Created alias `{alias.Name}` ➡️ `{alias.ReferencedTag.Name}`";
            var response = _responseService.GetSuccessfulResponse(message);
            return Response(response);
        }
        
        [AutoComplete("create")]
        public ValueTask TagNameAutocomplete(
            [Name("original")] AutoComplete<string> tagName) =>
            TagsAutocompletes.TagName(tagName, Context.GuildId, _tagService);
    }
}