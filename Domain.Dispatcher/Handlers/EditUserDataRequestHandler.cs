using Domain.Dispatcher.Requests;
using Domain.Dispatcher.Responses;
using Domain.Services.Core;
using MediatR;
using Qommon;

namespace Domain.Dispatcher.Handlers;

public class EditUserDataRequestHandler : IRequestHandler<EditUserDataRequest, EditUserDataResponse>
{
    private readonly IUserService _userService;

    public EditUserDataRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<EditUserDataResponse> Handle(EditUserDataRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetOrCreateAsync(request.UserId);

        user.AccessLevel = request.AccessLevel.GetValueOrDefault(user.AccessLevel);
        user.UserName = request.UserName.GetValueOrDefault(user.UserName);
        user.CustomTagLimit = request.CustomTagLimit.GetValueOrDefault(user.CustomTagLimit);

        user = await _userService.UpdateUserAsync(user);

        return new EditUserDataResponse
        {
            UserData = user
        };
    }
}