using Data.Entities.Users;

namespace Domain.Dispatcher.Responses;

public record EditUserDataResponse
{
    public required UserData UserData { get; init; }
}