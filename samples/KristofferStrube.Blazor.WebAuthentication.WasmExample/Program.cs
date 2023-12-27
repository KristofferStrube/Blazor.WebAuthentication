using KristofferStrube.Blazor.CredentialManagement;
using KristofferStrube.Blazor.WebAuthentication.WasmExample;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddKeyedScoped(typeof(WebAuthenticationClient), (_, _) => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.IsDevelopment() ? "https://localhost:7259/WebAuthentication/" : "https://kristoffer-strube.dk/API/WebAuthentication/")
});

builder.Services.AddCredentialsService();

// For communicating with the API.
builder.Services.AddScoped<WebAuthenticationClient>();

WebAssemblyHost app = builder.Build();

await app.Services.SetupErrorHandlingJSInterop();

await app.RunAsync();