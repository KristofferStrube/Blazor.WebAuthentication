using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;

public class PublicKeyCredentialJSON
{
    public static async Task<PublicKeyCredentialJSON> GetConcreteInstanceAsync(ValueReference authenticatorResponse)
    {
        authenticatorResponse.ValueMapper = new()
        {
            { "registrationresponsejson", async () => await authenticatorResponse.GetValueAsync<RegistrationResponseJSON>() },
            { "authenticationresponsejson", async () => await authenticatorResponse.GetValueAsync<AuthenticationResponseJSON>() }
        };

        object? result = await authenticatorResponse.GetValueAsync();
        return result is IJSObjectReference or null ? null! : (PublicKeyCredentialJSON)result;
    }
}


