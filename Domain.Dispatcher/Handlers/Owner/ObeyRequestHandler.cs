using Data.Entities.Users;
using Domain.Dispatcher.Requests.Owner;
using Domain.Dispatcher.Responses.Owner;
using Domain.Exceptions;
using Domain.Services.Core;
using MediatR;

namespace Domain.Dispatcher.Handlers.Owner;

public class ObeyRequestHandler : IRequestHandler<ObeyRequest, ObeyResponse>
{
    private readonly IUserService _userService;

    public ObeyRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ObeyResponse> Handle(ObeyRequest request, CancellationToken cancellationToken)
    {
        AccessException.ThrowIf(request.IsOwner is false);
        
        var user = await _userService.GetOrCreateAsync(request.UserId);
        user.AccessLevel = UserAccessLevel.Admin;
        user = await _userService.UpdateUserAsync(user);

        return new ObeyResponse
        {
            UpdatedUser = user
        };
    }
}