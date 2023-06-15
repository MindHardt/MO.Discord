using BotServices.Autocompletes.Core.Tags;
using BotServices.CQRS.Dispatcher.Core;
using BotServices.CQRS.Requests.Tags;
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
        private readonly IDiscordCommandDispatcher _discordCommandDispatcher;
        private readonly ITagViewAutocompleteProvider _autocompleteProvider;

        public TagAliasCommandModule(
            IDiscordCommandDispatcher discordCommandDispatcher, 
            ITagViewAutocompleteProvider autocompleteProvider)
        {
            _discordCommandDispatcher = discordCommandDispatcher;
            _autocompleteProvider = autocompleteProvider;
        }


        [SlashCommand("добавить")]
        [Description("Создает псевдоним для тега, т.е. новое название по которому к нему можно обращаться")]
        public async ValueTask<IResult> Create(
            [Name("тег"), Description("Название исходного тега")]
            string originalName,
            [Name("псевдоним"), Description("Новое название тега")]
            string newName)
            => await _discordCommandDispatcher.DispatchAsync(new CreateTagAliasRequest
            {
                TagName = originalName,
                AliasName = newName,
                Context = Context
            });

        [AutoComplete("добавить")]
        public ValueTask TagNameAutocomplete(
            [Name("тег")] AutoComplete<string> tagName) =>
            _autocompleteProvider.GetAutocomplete().CompleteAsync(tagName, Context);
    }
}