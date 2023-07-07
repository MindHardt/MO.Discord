using Domain.Dispatcher.Requests.Images;
using Domain.Dispatcher.Responses.Images;
using Domain.Factories.Core;
using Domain.Factories.Core.Images;
using MediatR;

namespace Domain.Dispatcher.Handlers.Image;

public class GetColorRequestHandler : IRequestHandler<GetColorRequest, GetColorResponse>
{
    private readonly IColorImageFactory _colorImageFactory;

    public GetColorRequestHandler(IColorImageFactory colorImageFactory)
    {
        _colorImageFactory = colorImageFactory;
    }

    public async Task<GetColorResponse> Handle(GetColorRequest request, CancellationToken cancellationToken)
    {
        return new GetColorResponse
        {
            Image = await _colorImageFactory.GetImageAsync(request.Color)
        };
    }
}