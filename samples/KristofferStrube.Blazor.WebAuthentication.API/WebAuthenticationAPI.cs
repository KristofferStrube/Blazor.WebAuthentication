using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography;

namespace KristofferStrube.Blazor.WebAuthentication.API;

public static class WebAuthenticationAPI
{
    public static IEndpointRouteBuilder MapWebAuthenticationAPI(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder
            .MapGroup("WebAuthentication");

        _ = group.MapGet("Register/{userName}", Register)
            .WithName("Register");

        return builder;
    }
    
    public static Ok<byte[]> Register(string userName)
    {
        return TypedResults.Ok(RandomNumberGenerator.GetBytes(32));
    }
}
