using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.CredentialManagement.Converters;

public class CredentialMediationRequirementConverter : JsonConverter<CredentialMediationRequirement>
{
    public override CredentialMediationRequirement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, CredentialMediationRequirement value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            CredentialMediationRequirement.Silent => "silent",
            CredentialMediationRequirement.Optional => "optional",
            CredentialMediationRequirement.Required => "required",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(CredentialMediationRequirement)}.")
        });
    }
}
