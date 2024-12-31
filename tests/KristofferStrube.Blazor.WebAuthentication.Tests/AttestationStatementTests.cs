﻿using FluentAssertions;
using FluentAssertions.Execution;
using System.Security.Cryptography;

namespace KristofferStrube.Blazor.WebAuthentication.Tests;

public class AttestationStatementTests
{
    [Fact]
    public void None_AttestationObject_CanBeParsed()
    {
        // Arrange
        string attestationObject = "o2NmbXRkbm9uZWdhdHRTdG10oGhhdXRoRGF0YViUfcfg03b1kxLHnF0mpy6nRsulHtqgscojQPUAQxyo9mBdAAAAAOqbjWZNAR0hPOS2tIy1ddQAEJmvgOPCmkesIY265qnCHCelAQIDJiABIVgg8V97cvCnVsupp29WapXqpis8L2\u002BGudY63q1jWBqhvF4iWCD0fuS8lSlnZ2OdyzW\u002BDIhlbKVVw16C/tyErEv0EiCAyg==";

        // Act
        var result = AttestationStatement.ReadFromBase64EncodedAttestationStatement(attestationObject);

        // Assert
        _ = result.Should().BeOfType<NoneAttestationStatement>();
    }

    [Fact]
    public void TPM_AttestationObject_CanBeParsed()
    {
        // Arrange
        string attestationObject = "o2NmbXRjdHBtZ2F0dFN0bXSmY2FsZzn//mNzaWdZAQAmZOqDKka896vNDbGN9vP4rYch9kKFikscMhNoAxQ9epANhUcknLyCLk8kLsOG1XxUfdlF9gKaBLJ6tYYwq\u002BUzQA2EKj9rRg6I4aZmKcamQVfz39VWYKUwL/MfCd8M1758Z9iVzEe3/nTYUi9NOQ5yoNdzYYTMOo9\u002BUZR4vF6ZMXW2iwtY/SY0loVDREIUGSZbAxFauLimnZEbAp0XvDtneOWnrdZ3EhclvDmlKBRb46shBpawsA1rrEshxvkDrIctaEg20tGILluPewSyAzGtb2JtKc/EqAKv9o788CPzCG71gi6ZQ1qkqukUkIRGHHFqjod4i3QhQ1/boCTwyWBeY3ZlcmMyLjBjeDVjglkFxDCCBcAwggOooAMCAQICED7yGspsgUQ2oC5PoqMycLYwDQYJKoZIhvcNAQELBQAwQTE/MD0GA1UEAxM2RVVTLVNUTS1LRVlJRC1GQjE3RDcwRDczNDg3MEU5MTlDNEU4RTYwMzk3NUU2NjRFMEU0M0RFMB4XDTIzMTIwNDEzMDYyMVoXDTI3MDYwMzE5NDAyNFowADCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMZB7aSfGaYByS/zXUXhPOv\u002BLwoKAjasTZ7hsE027y9hBe4H2zt9v/g/ZdCN0ItoHjmCm1GClbq7uhZUKnQLVZvANZj8mp8fPykkDQ7\u002BUtLZR128xwR13YuifPKoShDe9BlFnT2FKEAau/n2Kfe7Y1z/z/vju7T68DKSegdHkJIInBoHkl5jIfz/epQ7L0ZgTr9zZ\u002B0ZqIMOsD2npm3CILDSmep8my13Wb4EklS6UqDVbLtMjoZZdCvdglBAzSPMLzW2cYi7SRCIjFQ69jVf8aWIeFwEflHJ/U1PNXIsOEgQHU727YmUDEojHoBy/DbrYIm1rOFXebss\u002BrFJ/tj2KV8CAwEAAaOCAfMwggHvMA4GA1UdDwEB/wQEAwIHgDAMBgNVHRMBAf8EAjAAMG0GA1UdIAEB/wRjMGEwXwYJKwYBBAGCNxUfMFIwUAYIKwYBBQUHAgIwRB5CAFQAQwBQAEEAIAAgAFQAcgB1AHMAdABlAGQAIAAgAFAAbABhAHQAZgBvAHIAbQAgACAASQBkAGUAbgB0AGkAdAB5MBAGA1UdJQQJMAcGBWeBBQgDMFkGA1UdEQEB/wRPME2kSzBJMRYwFAYFZ4EFAgEMC2lkOjUzNTQ0RDIwMRcwFQYFZ4EFAgIMDFNUMzNIVFBIQUhFNDEWMBQGBWeBBQIDDAtpZDowMDAxMDMwMTAfBgNVHSMEGDAWgBQ4pJZObjM\u002BjRpJDnX8A7aFbZT8iDAdBgNVHQ4EFgQUBtuLgW9EjuajEOxLkmSejflmfh8wgbIGCCsGAQUFBwEBBIGlMIGiMIGfBggrBgEFBQcwAoaBkmh0dHA6Ly9hemNzcHJvZGV1c2Fpa3B1Ymxpc2guYmxvYi5jb3JlLndpbmRvd3MubmV0L2V1cy1zdG0ta2V5aWQtZmIxN2Q3MGQ3MzQ4NzBlOTE5YzRlOGU2MDM5NzVlNjY0ZTBlNDNkZS84ZTJjZjI5NS1kMjIzLTQ5NmEtOTdlYy0yNTc2OTVjNmMyZmMuY2VyMA0GCSqGSIb3DQEBCwUAA4ICAQBo69fHfVEtul5n42xE/dapb7Jj7/eoCrSSUQKq3UjHbwv9Wr\u002B03G/VfaLp9HoXXxxDxPz8KuMrsAZs93FZ9QfQ2tdNQoay5hD5lJayeQ\u002BJB/fEgkwFH0QDTNX\u002BzaqZVxjOG4c79ATKLPy4V6tGxskO7NExVXwWniYALCeF\u002BwiwtDEeyvZsG0sdLSwwYuztHQNQ1dtqcuHkItNmmECgyivKTonO3qObOk5sn9JKbTRdGRVV/aATrHNXvFuem2q1LE/xL8U\u002B4ezKVyK6l0QyWS4oHj2cYSsqPOETzmZzPjdgQBsiw4A4GJcuhLELZ0tQZvyOfofeq6X\u002B383DRpdaUedKh64uzrvGJWVM1ZvBUE9LlpBznBQ3yz1YUgK32V9cnTSo\u002B0XSlXEPTOfmeBiS4a/T0qsHdaIeLYwVYmEkNwjImHR84HZzAZfife72PUxZoOimQRJdo6iEJ2eFQ5OVPRcMm9usXqlPQbDtCbpyPOZRI\u002B\u002B/XYt3zX8BM8z6A3vP6ldmS1MglyLZwAFs2GJHkohOFFNWbc3DORz59K\u002Bb4lnfExikhUqu97ydWYV1YO79g5ECdYv7Nqoa/xgRjvzXLjA7I\u002BwbYJjy74cwfyt/u4pO6dN/4tT7X6RycEBEQmpWPtbMv2lp56fXFhWMLYlvqr4KK3z/ftEFgRBd8NGBufnKLFkG7zCCBuswggTToAMCAQICEzMAAAUtM4db5/ICoa8AAAAABS0wDQYJKoZIhvcNAQELBQAwgYwxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xNjA0BgNVBAMTLU1pY3Jvc29mdCBUUE0gUm9vdCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkgMjAxNDAeFw0yMTA2MDMxOTQwMjRaFw0yNzA2MDMxOTQwMjRaMEExPzA9BgNVBAMTNkVVUy1TVE0tS0VZSUQtRkIxN0Q3MEQ3MzQ4NzBFOTE5QzRFOEU2MDM5NzVFNjY0RTBFNDNERTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAKvu5BtXmmeYhA9MHXP9BXKRWLoSn352DZWpcmSxtASbp2evJt5EiGuyHX637koanGphUUmKk26USKD19nyamCCQy6Wh4/U01DICR3gcaR7nsKE\u002B/uL3ratdR0xwpx/lO6WZw6bvuqsDSVFebZeOYBk310utv4kiMtDYC91\u002B0/JdjSGQtaYvNZJz7NfhNxtOvmLsNl9gjZOQOsF45SjNcRh/0S62qF4g4dM7q1/HuFlWLlDwNzAjn07nE2gNHjJ2zCxCZkh0PoLakKJZZRe1O0CfyQP9cCoPKk7nGfMpKn8wy\u002BRisMzaBopjl7NiyManoUT51qsFzbPNN3vUnqNeRPl9u/PteYMM7Agx73MVX5/76qA49mqrnP/XNpHUD/B6k9Ti2vtV5rnYNFtedbxDwEqdNcMNk068jBhecuFdPdKdatwWDz7oczxt5YyJTGMSuDPRZGHtAgl6Y5lHsFSaozpz/QmlRmlSni5MYyy0Kol7qrcLjgzAr\u002BwG1QHTlRRAyeaUAU0Op1c9yPVYSpwzmOsyI3swQKIZIAhHj8MVBUDsvBotf7GULUKJppfw5B43khwpNZVoUT6wKyYXSKekxPTsxdz7azBCPXWt9qJO4ZIzLzhOFq8eyXySFk92zuTb2gmwWXNLXsjMOm6hqk3sPm9DL/Rn9aSAi7SKJem4ibgNAgMBAAGjggGOMIIBijAOBgNVHQ8BAf8EBAMCAoQwGwYDVR0lBBQwEgYJKwYBBAGCNxUkBgVngQUIAzAWBgNVHSAEDzANMAsGCSsGAQQBgjcVHzASBgNVHRMBAf8ECDAGAQH/AgEAMB0GA1UdDgQWBBQ4pJZObjM\u002BjRpJDnX8A7aFbZT8iDAfBgNVHSMEGDAWgBR6jArOL0hiF\u002BKU0a5VwVLscXSkVjBwBgNVHR8EaTBnMGWgY6Bhhl9odHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpb3BzL2NybC9NaWNyb3NvZnQlMjBUUE0lMjBSb290JTIwQ2VydGlmaWNhdGUlMjBBdXRob3JpdHklMjAyMDE0LmNybDB9BggrBgEFBQcBAQRxMG8wbQYIKwYBBQUHMAKGYWh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMvY2VydHMvTWljcm9zb2Z0JTIwVFBNJTIwUm9vdCUyMENlcnRpZmljYXRlJTIwQXV0aG9yaXR5JTIwMjAxNC5jcnQwDQYJKoZIhvcNAQELBQADggIBABjK2\u002B9pxH4S\u002B6fyCAKHHgROS5UvqzLkSZNd0F\u002B3bPJ9q1\u002BkAdUmk0\u002BF6lXpJcGTXG2tcX0VpOoxHYeuugPTYE2YsmVSd4l\u002B/sKPdDKPs5ZoJBYemEUsYjp0I2NUrSjQqSM7OTLUe/wdSEUaD1QIfQ1QmSSoGg8WqR23yswykOrkRomLRJqIQrI4Iyd1pSMhRVkizM/6asjyy/xCi3J29BnNAZFFUnH0fcfR9R6t2MSxo84aYvV8n0cdyFyM/L654kdUZcyqn4R0lfnemOxej4e9/pQLyP0qY1mfJ4TRiCTJ\u002BeG7VmC5tdH9Ol5QhiVsqWYBX6rF8hd7RSLDBr4HF8ve1IF1Nsg0qRtfPjAiax8q6TE/rpe0YMROHRcanBufX7U16idX/l/y6aOyvnezoCqEK1IM8YAE8/GF7RQJN6xNXB171vVudlet\u002B3gIoSp/flCgtIo81V6wRl\u002BCKtaNTNGX0frRaDp\u002BE7I3ullpJqhK5KtQE7AKGUeh8nc9LAKVW0FAnlrs96eDHbB/F53EBFc\u002BUtbYtSpXzx10RvqtctsWOtl5w5dEn6Pl1FugG\u002BKZ/fMWrDAk54WqyojOethgS1SbZb1dwzEixAZeUn7hjPqmI0IE0JJ13HJLPYLgpjWjf29n6NQ4rG7n134zNw1WjKseyCaUnN8AJ8aKDKwGBqTzZ3B1YkFyZWFYdgAjAAsABAQyACANlDe44jA/Lh9WlLkzGiFbLRtCkZsQcR2d/3fxFh9wiAAQABAAAwAQACB0s8Wqh9qsmgu0bkwMOT0zJcVZKRyo\u002BsPiLJIvTSC73gAgTvavLjnRvA6xFthlDlC5tA2dXB47D9/9wZLnULsVutZoY2VydEluZm9Yof9UQ0eAFwAiAAtowgBgfMtWkhgfWcncjqx1bT3ixhZn3bI3wB4Bo80lfwAUs0Bxm1z0X2zPSTLgW8WDMbWRPIMAAAAAObSrnyVm6fQt6eQWAQav\u002B508WuMSACIAC4zXXU60nKWkl0AQ\u002BkG07vqTyK/LhvehDx8iJJG4b\u002BGsACIAC5agueugLwosXuToSHXWGD6qRrs/swm8tVrK7FqALNzpaGF1dGhEYXRhWKR9x\u002BDTdvWTEsecXSanLqdGy6Ue2qCxyiNA9QBDHKj2YEUAAAAAnd0YF69aRnKiuT492VAAqQAgDFOCICwZ7LYEftbR3KVBx9mVeylbUS2zr8o7XBxE2BWlAQIDJiABIVggdLPFqofarJoLtG5MDDk9MyXFWSkcqPrD4iySL00gu94iWCBO9q8uOdG8DrEW2GUOULm0DZ1cHjsP3/3BkudQuxW61g==";
        string authenticatorData = "fcfg03b1kxLHnF0mpy6nRsulHtqgscojQPUAQxyo9mBFAAAAAJ3dGBevWkZyork\u002BPdlQAKkAIAxTgiAsGey2BH7W0dylQcfZlXspW1Ets6/KO1wcRNgVpQECAyYgASFYIHSzxaqH2qyaC7RuTAw5PTMlxVkpHKj6w\u002BIski9NILveIlggTvavLjnRvA6xFthlDlC5tA2dXB47D9/9wZLnULsVutY=";
        string clientDataJSON = "eyJ0eXBlIjoid2ViYXV0aG4uY3JlYXRlIiwiY2hhbGxlbmdlIjoiQ3ZGc0FNWV80SmRUQ1d4MG9HVmNsdnd0eW95RV9ZZ05qUlRGcEFoTkxtayIsIm9yaWdpbiI6Imh0dHBzOi8va3Jpc3RvZmZlcnN0cnViZS5naXRodWIuaW8iLCJjcm9zc09yaWdpbiI6ZmFsc2UsIm90aGVyX2tleXNfY2FuX2JlX2FkZGVkX2hlcmUiOiJkbyBub3QgY29tcGFyZSBjbGllbnREYXRhSlNPTiBhZ2FpbnN0IGEgdGVtcGxhdGUuIFNlZSBodHRwczovL2dvby5nbC95YWJQZXgifQ==";

        byte[] clientDataJSONBytes = Convert.FromBase64String(clientDataJSON);
        var hasher = SHA256.Create();
        byte[] clientDataHash = hasher.ComputeHash(clientDataJSONBytes);

        // Act
        var result = AttestationStatement.ReadFromBase64EncodedAttestationStatement(attestationObject);

        // Assert
        using (new AssertionScope())
        {
            TPMAttestationStatement attestationStatement = result.Should().BeOfType<TPMAttestationStatement>().Subject;

            _ = attestationStatement.Verify(Convert.FromBase64String(authenticatorData), clientDataHash).Should().BeTrue();
        }
    }
}