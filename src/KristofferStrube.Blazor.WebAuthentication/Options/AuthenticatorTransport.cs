using KristofferStrube.Blazor.WebAuthentication.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

[JsonConverter(typeof(AuthenticatorTransportConverter))]
public enum AuthenticatorTransport
{
    Usb,
    Nfc,
    Ble,
    Internal,
}
