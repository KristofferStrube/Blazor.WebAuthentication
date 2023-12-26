using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

public class AuthenticatorAssertionResponse : AuthenticatorResponse, IJSCreatable<AuthenticatorAssertionResponse>
{
    public static Task<AuthenticatorAssertionResponse> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult<AuthenticatorAssertionResponse>(new(jSRuntime, jSReference));
    }

    public AuthenticatorAssertionResponse(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<IJSObjectReference> GetSignatureAsync()
    {
        IJSObjectReference helper = await webAuthenticationHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "signature");
    }
}
