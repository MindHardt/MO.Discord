using System.Drawing;
using Domain.Factories.Core.Images;
using Domain.Models;

namespace Domain.Factories.Default.Images;

public class WebColorImageFactory : IColorImageFactory
{
    private readonly HttpClient _httpClient;
    private const string Url = "https://www.colorhexa.com/{0}";

    public WebColorImageFactory(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<NamedStream> GetImageAsync(Color color)
    {
        var fileName = GetFileName(color);
        var uri = string.Format(Url, fileName);

        return new NamedStream(await _httpClient.GetStreamAsync(uri), fileName);
    }

    private static string GetFileName(Color color) => $"{GetHex(color)}.png";
    private static string GetHex(Color color) => Convert.ToString(color.ToArgb(), toBase: 16)[..6];
}