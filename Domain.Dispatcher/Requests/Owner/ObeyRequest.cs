using Disqord;
using Domain.Dispatcher.Responses.Owner;
using MediatR;

namespace Domain.Dispatcher.Requests.Owner;

public class ObeyRequest : IRequest<ObeyResponse>
{
    public required Snowflake UserId { get; init; }
    public required bool IsOwner { get; init; }
}