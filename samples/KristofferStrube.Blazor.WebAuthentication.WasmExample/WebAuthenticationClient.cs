using KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;
using System.Net.Http.Json;

namespace KristofferStrube.Blazor.WebAuthentication.WasmExample;

public class WebAuthenticationClient
{
    private readonly HttpClient httpClient;

    public WebAuthenticationClient([FromKeyedServices(typeof(WebAuthenticationClient))]HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<byte[]?> RegisterChallenge(string userName)
    {
        return await httpClient.GetFromJsonAsync<byte[]>($"RegisterChallenge/{userName}");
    }

    public async Task<bool> Register(string userName, RegistrationResponseJSON registrationResponse)
    {
        HttpResponseMessage result = await httpClient.PostAsJsonAsync($"Register/{userName}", registrationResponse);
        return await result.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<ValidateCredentials?> ValidateChallenge(string userName)
    {
        return await httpClient.GetFromJsonAsync<ValidateCredentials?>($"ValidateChallenge/{userName}");
    }
    public class ValidateCredentials(byte[] challenge, List<byte[]> credentials)
    {
        public byte[] Challenge { get; set; } = challenge;
        public List<byte[]> Credentials { get; set; } = credentials;
    }

    public async Task<bool> Validate(string userName, AuthenticationResponseJSON authenticationResponse)
    {
        HttpResponseMessage result = await httpClient.PostAsJsonAsync($"Validate/{userName}", authenticationResponse);
        return await result.Content.ReadFromJsonAsync<bool>();
    }
}
