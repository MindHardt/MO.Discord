using Data.Entities.Users;

namespace Domain.Dispatcher.Responses.Owner;

public class ObeyResponse
{
    public required UserData UpdatedUser { get; init; }
}