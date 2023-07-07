using Data.Entities.Users;
using Disqord;
using Domain.Dispatcher.Responses;
using MediatR;
using Qommon;

namespace Domain.Dispatcher.Requests;

public record EditUserDataRequest : IRequest<EditUserDataResponse>
{
    public required Snowflake UserId { get; init; }
    public Optional<AccessLevel> AccessLevel { get; init; }
    public Optional<string> UserName { get; init; }
    public Optional<int?> CustomTagLimit { get; init; }
}