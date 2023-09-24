using KristofferStrube.Blazor.CredentialManagement.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CredentialManagement;

public class CredentialsService(IJSRuntime jSRuntime)
{
    public async Task<CredentialsContainer> GetCredentialsAsync()
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("navigator.credentials.valueOf");
        return new CredentialsContainer(jSRuntime, jSInstance);
    }

    public async Task<bool> IsSupportedAsync()
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        return await helper.InvokeAsync<bool>("isSupported");
    }
}


