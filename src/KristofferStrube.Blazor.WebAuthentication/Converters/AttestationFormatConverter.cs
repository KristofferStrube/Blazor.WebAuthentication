using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication.Converters;

public class AttestationFormatConverter : JsonConverter<AttestationFormat>
{
    public override AttestationFormat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "packed" => AttestationFormat.Packed,
            "tpm" => AttestationFormat.TPM,
            "android-key" => AttestationFormat.AndroidKey,
            "android-safetynet" => AttestationFormat.AndroidSafetyNet,
            "fido-u2f" => AttestationFormat.FidoU2F,
            "apple" => AttestationFormat.Apple,
            "none" => AttestationFormat.None,
            var value => throw new ArgumentException($"Value '{value}' was not a valid {nameof(AttestationFormat)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, AttestationFormat value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            AttestationFormat.Packed => "packed",
            AttestationFormat.TPM => "tpm",
            AttestationFormat.AndroidKey => "android-key",
            AttestationFormat.AndroidSafetyNet => "android-safetynet",
            AttestationFormat.FidoU2F => "fido-u2f",
            AttestationFormat.Apple => "apple",
            AttestationFormat.None => "none",
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(AttestationFormat)}.")
        });
    }
}
