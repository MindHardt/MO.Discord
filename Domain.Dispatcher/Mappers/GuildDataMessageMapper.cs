using Data.Entities;
using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Mappers.Common;
using Domain.Factories.Core;

namespace Domain.Dispatcher.Mappers;

public class GuildDataMessageMapper : IMessageMapper<GuildData>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;
    private readonly ITypeMapper<bool, string> _booleanMapper;

    public GuildDataMessageMapper(
        IDiscordResponseFactory discordResponseFactory, 
        ITypeMapper<bool, string> booleanMapper)
    {
        _discordResponseFactory = discordResponseFactory;
        _booleanMapper = booleanMapper;
    }

    public TMessage MapAs<TMessage>(GuildData source) where TMessage : LocalMessageBase, new()
    {
        var embedFields = new[]
        {
            new LocalEmbedField()
                .WithName(MappingResources.GuildData_InlineTags)
                .WithValue(Markdown.Code(_booleanMapper.Map(source.InlineTagsEnabled))),
            new LocalEmbedField()
                .WithName(MappingResources.GuildData_InlineTagPrefix)
                .WithValue(Markdown.Code(source.InlineTagsPrefix)),
            new LocalEmbedField()
                .WithName(MappingResources.GuildData_AdultAllowed)
                .WithValue(Markdown.Code(_booleanMapper.Map(source.AdultAllowed)))
        };
        var title = string.Format(MappingResources.GuildData_GuildName, Markdown.Code(source.GuildName));
        var footer = string.Format(MappingResources.GuildData_GuildId, source.GuildId);

        return _discordResponseFactory.GetSuccessfulMessageResponse<TMessage>()
            .AddEmbed(new LocalEmbed()
                .WithTitle(title)
                .WithFields(embedFields)
                .WithFooter(footer));
    }
}