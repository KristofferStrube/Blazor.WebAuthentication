using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

public class AuthenticatorAttestationResponse : AuthenticatorResponse, IJSCreatable<AuthenticatorAttestationResponse>
{
    public static Task<AuthenticatorAttestationResponse> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult<AuthenticatorAttestationResponse>(new(jSRuntime, jSReference));
    }

    public AuthenticatorAttestationResponse(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<IJSObjectReference> GetPublicKeyAsync()
    {
        return await JSReference.InvokeAsync<IJSObjectReference>("getPublicKey");
    }
}
