using System.Net.Http.Json;

namespace KristofferStrube.Blazor.WebAuthentication.WasmExample;

public class WebAuthenticationClient
{
    private readonly HttpClient httpClient;

    public WebAuthenticationClient([FromKeyedServices(typeof(WebAuthenticationClient))]HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<byte[]?> Register(string userName)
    {
        return await httpClient.GetFromJsonAsync<byte[]>($"Register/{userName}");
    }
}
