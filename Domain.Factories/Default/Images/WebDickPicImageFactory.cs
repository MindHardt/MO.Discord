using Domain.Factories.Core.Images;
using Domain.Models;

namespace Domain.Factories.Default.Images;

public class WebDickPicImageFactory : IDickPicImageFactory
{
    private readonly HttpClient _httpClient;
    private const string Url = "";

    public WebDickPicImageFactory(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<NamedStream> GetDickPicImageAsync()
    {
        throw new NotImplementedException();
    }

    private static string GetUrl() => "";
}