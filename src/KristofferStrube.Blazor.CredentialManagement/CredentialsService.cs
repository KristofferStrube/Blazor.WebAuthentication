using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CredentialManagement;

public class CredentialsService(IJSRuntime jSRuntime)
{
    public async Task<CredentialsContainer> GetCredentialsAsync()
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("navigator.credentials.valueOf");
        return new CredentialsContainer(jSRuntime, jSInstance);
    }
}


