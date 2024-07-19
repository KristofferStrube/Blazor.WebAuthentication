using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

public class CollectedClientData
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("challenge")]
    public required string Challenge { get; set; }

    [JsonPropertyName("origin")]
    public required string Origin { get; set; }

    [JsonPropertyName("topOrigin")]
    public string? TopOrigin { get; set; }

    [JsonPropertyName("crossOrigin")]
    public bool CrossOrigin { get; set; }
}
