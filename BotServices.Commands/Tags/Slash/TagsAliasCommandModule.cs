using BotServices.Entities.Tags;
using BotServices.Factories.Core;
using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Commands.Tags.Slash;

public partial class TagsCommandModule
{
    [SlashGroup("псевдоним")]
    public class TagAliasCommandModule : DiscordApplicationGuildModuleBase
    {
        private readonly ITagService _tagService;
        private readonly IDiscordResponseFactory _discordResponseFactory;
        private readonly ITagFactory _tagFactory;

        public TagAliasCommandModule(
            ITagService tagService, 
            IDiscordResponseFactory discordResponseFactory, 
            ITagFactory tagFactory)
        {
            _tagService = tagService;
            _discordResponseFactory = discordResponseFactory;
            _tagFactory = tagFactory;
        }


        [SlashCommand("добавить")]
        [Description("Создает псевдоним для тега, т.е. новое название по которому к нему можно обращаться")]
        public async ValueTask<IResult> Create(
            [Name("тег"), Description("Название исходного тега")]
            string originalName,
            [Name("псевдоним"), Description("Новое название тега")]
            string newName)
        {
            if (_tagService.CheckTagName(newName) is false)
                return Results.Failure("Это имя недопустимо для тега.");
            
            Snowflake guildId = Context.GuildId;

            Tag? tag = await _tagService.GetTagAsync(originalName, guildId);

            if (tag is null) return Results.Failure($"Тег '{originalName}' не найден");

            TagMessage tagMessage = tag.GetTagMessage();
            Snowflake ownerId = Context.AuthorId;

            TagAlias alias = _tagFactory.CreateTagAlias(tagMessage, newName, ownerId, guildId);

            await _tagService.SaveTagAsync(alias, ownerId, await Bot.IsOwnerAsync(ownerId));

            var message = $"Создал псевдоним`{alias.Name}` ➡️ `{alias.ReferencedTag.Name}`";
            var response = _discordResponseFactory.GetSuccessfulResponse(message);
            return Response(response);
        }
        
        [AutoComplete("добавить")]
        public ValueTask TagNameAutocomplete(
            [Name("тег")] AutoComplete<string> tagName) =>
            TagsAutocompletes.TagName(tagName, Context.GuildId, _tagService);
    }
}