using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.Converters;

public class AttestationConveyancePreferenceConverter : JsonConverter<AttestationConveyancePreference>
{
    public override AttestationConveyancePreference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "none" => AttestationConveyancePreference.None,
            "indirect" => AttestationConveyancePreference.Indirect,
            "direct" => AttestationConveyancePreference.Direct,
            "enterprise" => AttestationConveyancePreference.Enterprise,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(PublicKeyCredentialType)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, AttestationConveyancePreference value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            AttestationConveyancePreference.None => "none",
            AttestationConveyancePreference.Indirect => "indirect",
            AttestationConveyancePreference.Direct => "direct",
            AttestationConveyancePreference.Enterprise => "enterprise",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(PublicKeyCredentialType)}.")
        });
    }
}
