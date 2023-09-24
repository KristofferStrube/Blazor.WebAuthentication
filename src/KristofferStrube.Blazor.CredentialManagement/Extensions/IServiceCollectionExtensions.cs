using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.CredentialManagement;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCredentialsService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<CredentialsService>();
    }
}