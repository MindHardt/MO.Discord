using Domain.Models;

namespace Domain.Factories.Core.Images;

public interface IDickPicImageFactory
{
    public Task<NamedStream> GetDickPicImageAsync();
}