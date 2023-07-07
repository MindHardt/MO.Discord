using System.Drawing;
using Domain.Dispatcher.Responses.Images;
using MediatR;

namespace Domain.Dispatcher.Requests.Images;

public record GetColorRequest : IRequest<GetColorResponse>
{
    public required Color Color { get; init; }
}