using Data.Entities.Users;
using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Mappers.Common;
using Domain.Factories.Core;
using Domain.Services.Core;

namespace Domain.Dispatcher.Mappers;

public class UserDataMessageMapper: IMessageMapper<UserData>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;
    private readonly IUserService _userService;
    private readonly ITypeMapper<bool, string> _booleanMapper;

    public UserDataMessageMapper(
        IDiscordResponseFactory discordResponseFactory, 
        IUserService userService, 
        ITypeMapper<bool, string> booleanMapper)
    {
        _discordResponseFactory = discordResponseFactory;
        _userService = userService;
        _booleanMapper = booleanMapper;
    }
    
    public TMessage MapAs<TMessage>(UserData source) where TMessage : LocalMessageBase, new()
    {
        var embedFields = new[]
        {
            new LocalEmbedField()
                .WithName(MappingResources.UserData_AccessLevel)
                .WithValue(Markdown.Code(source.AccessLevel.ToReadable())),
            new LocalEmbedField()
                .WithName(MappingResources.UserData_TagLimit)
                .WithValue(Markdown.Code(_userService.GetTagLimit(source)?.ToString() ?? "-")),
            new LocalEmbedField()
                .WithName(MappingResources.UserData_CustomTagLimit)
                .WithValue(Markdown.Code(_booleanMapper.Map(source.CustomTagLimit is not null))),
        };
        var title = string.Format(MappingResources.UserData_UserName, Markdown.Code(source.UserName));
        var footer = string.Format(MappingResources.UserData_UserId, source.UserId);

        return _discordResponseFactory.GetSuccessfulMessageResponse<TMessage>()
            .AddEmbed(new LocalEmbed()
                .WithTitle(title)
                .WithFields(embedFields)
                .WithFooter(footer));
    }
}