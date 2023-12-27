using System.Text.Json.Serialization;
using System.Text.Json;

namespace KristofferStrube.Blazor.WebAuthentication.JSONRepresentations;

public class AuthenticationExtensionsClientOutputsJSON
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}
