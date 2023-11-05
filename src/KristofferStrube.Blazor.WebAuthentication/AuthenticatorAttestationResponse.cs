using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebAuthentication;

public class AuthenticatorAttestationResponse : IJSCreatable<AuthenticatorAttestationResponse>
{
    public IJSObjectReference JSReference => throw new NotImplementedException();

    public IJSRuntime JSRuntime => throw new NotImplementedException();

    public static Task<AuthenticatorAttestationResponse> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        throw new NotImplementedException();
    }
}
