using KristofferStrube.Blazor.WebAuthentication.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebAuthentication;

/// <summary>
/// Authenticators may implement various transports for communicating with clients.
/// This enum defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.
/// </summary>
/// <remarks><see href="https://w3c.github.io/webauthn/#enum-transport">See the API definition here</see>.</remarks>
[JsonConverter(typeof(AuthenticatorTransportConverter))]
public enum AuthenticatorTransport
{
    /// <summary>
    /// Indicates the respective authenticator can be contacted over removable USB.
    /// </summary>
    Usb,
    /// <summary>
    /// Indicates the respective authenticator can be contacted over Near Field Communication (NFC).
    /// </summary>
    Nfc,
    /// <summary>
    /// Indicates the respective authenticator can be contacted over Bluetooth Smart (Bluetooth Low Energy / BLE).
    /// </summary>
    Ble,
    /// <summary>
    /// Indicates the respective authenticator can be contacted over ISO/IEC 7816 smart card with contacts.
    /// </summary>
    SmartCard,
    /// <summary>
    /// Indicates the respective authenticator can be contacted using a combination of (often separate) data-transport and proximity mechanisms.
    /// This supports, for example, authentication on a desktop computer using a smartphone.
    /// </summary>
    Hybrid,
    /// <summary>
    /// Indicates the respective authenticator is contacted using a client device-specific transport, i.e., it is a platform authenticator.
    /// These authenticators are not removable from the client device.
    /// </summary>
    Internal,
}
