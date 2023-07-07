using System.Drawing;
using Domain.Models;

namespace Domain.Factories.Core.Images;

public interface IColorImageFactory
{
    /// <summary>
    /// Gets file stream with image of solid <paramref name="color"/>.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public Task<NamedStream> GetImageAsync(Color color);
}